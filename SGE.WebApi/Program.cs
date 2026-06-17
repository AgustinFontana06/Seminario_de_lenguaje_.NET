using SGE.Aplicacion.Extensiones;
using SGE.Infraestructura.Extensiones;
using SGE.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAplicacion();
builder.Services.AddInfraestructura(builder.Configuration);

var app = builder.Build();

app.MapExpedientesEndpoints();
app.MapTramitesEndpoints();

app.Run();