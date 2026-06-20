using SGE.Aplicacion.Abstracciones;
using SGE.Infraestructura.Extensiones;
using SGE.Aplicacion.Extensiones;
using SGE.Aplicacion.Usuarios.Login;
using SGE.Aplicacion.Usuarios.Modificar;

using SGE.WebApi;
using SGE.Aplicacion.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection; // esto para inyectarmejor las dependencias nose si va aca


var builder = WebApplication.CreateBuilder(args);// modifcar muchos errores

builder.Services.AddAplicacion(builder.Configuration);
builder.Services.AddInfraestructura(builder.Configuration);
builder.Services.AddScoped<ITokenProvider, JwtTokenProvider>();
builder.Services.AddScoped<LoginUsuarioUseCase>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(opciones =>
 opciones.TokenValidationParameters = new TokenValidationParameters
 {
 ValidateIssuer = true, // Validar quién lo emitió
 ValidateAudience = true, // Validar para quién es
 ValidateLifetime = true, // Validar que no esté vencido
 ValidateIssuerSigningKey = true, // ¡Vital! Validar la firma criptográfica

 ValidIssuer = builder.Configuration["Jwt:Issuer"],
 ValidAudience = builder.Configuration["Jwt:Audience"],
 IssuerSigningKey = new SymmetricSecurityKey(
 Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
 });

// Agregamos el servicio de Autorización (para manejar los roles luego)
builder.Services.AddAuthorization();


var app = builder.Build();
// "Descubre quién es" leyendo el token de la cabecera HTTP
app.UseAuthentication();
// "Decide si tiene permiso" para acceder a la ruta solicitada
app.UseAuthorization();
app.MapPost("/api/login", (LoginUsuarioRequest request, LoginUsuarioUseCase useCase) =>
{
 // El middleware de excepciones que hicimos atrapará automáticamente
 // la AutorizacionException si la clave está mal y devolverá un
 // HTTP 403.
 var response = useCase.Ejecutar(request);
 return Results.Ok(response);
});
app.MapPost("/", ( // esot raro a chequear so es asi tuve que modficar un poco con lo del profe
 ModificarMisDatosRequest request,
 ClaimsPrincipal user, // <- Acá .NET deja los datos del JWT ya validados
 ModificarMisDatosUseCase useCase) =>
{
 // Extraemos el claim "ID" que pusimos adentro del token en el login
 var userIdString = user.FindFirst("ID")?.Value;
 var idUsuario = Guid.Parse(userIdString!);
 var response = useCase.Ejecutar(request, idUsuario);
 return Results.Created($"/api/alumnos/{response.Id}", response);
})
.RequireAuthorization(); 

app.MapExpedientesEndpoints();
app.MapTramitesEndpoints();///

app.Run();