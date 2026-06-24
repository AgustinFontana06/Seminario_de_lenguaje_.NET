using SGE.Aplicacion.Abstracciones;
using SGE.Infraestructura.Extensiones;
using SGE.Aplicacion.Extensiones;
using SGE.WebApi;
using SGE.WebApi.Excepciones;
using SGE.WebApi.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SGE.Infraestructura.Datos;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

//agrego el formato de excepciones globales
builder.Services.AddProblemDetails();
//registro el manejador personalizado
builder.Services.AddExceptionHandler<ManejadorDeExcepcionesGlobales>();

builder.Services.AddAplicacion(builder.Configuration);
builder.Services.AddInfraestructura(builder.Configuration);
builder.Services.AddScoped<ITokenProvider, JwtTokenProvider>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(opciones =>
 opciones.TokenValidationParameters = new TokenValidationParameters
 {
     ValidateIssuer = true,
     ValidateAudience = true,
     ValidateLifetime = true,
     ValidateIssuerSigningKey = true,
     ValidIssuer = builder.Configuration["Jwt:Issuer"],
     ValidAudience = builder.Configuration["Jwt:Audience"],
     IssuerSigningKey = new SymmetricSecurityKey(
         Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
 });

builder.Services.AddAuthorization();

var app = builder.Build();

//agregamos middleware al principio del pipeline
app.UseExceptionHandler();

//inicializamos base de datos:
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GestionContext>();
    GestionSqlite.Inicializar(context);
}


app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapUsuariosEndpoints();
app.MapExpedientesEndpoints();
app.MapTramitesEndpoints();

app.MapGet("/", () => "¡La API está funcionando correctamente!");

app.Run();