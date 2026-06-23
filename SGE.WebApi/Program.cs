using SGE.Aplicacion.Abstracciones;
using SGE.Infraestructura.Extensiones;
using SGE.Aplicacion.Extensiones;
using SGE.WebApi;
using SGE.WebApi.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapUsuariosEndpoints();
app.MapExpedientesEndpoints();
app.MapTramitesEndpoints();

app.Run();