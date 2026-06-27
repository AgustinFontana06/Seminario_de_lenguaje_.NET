using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGE.Aplicacion.Abstracciones;
using SGE.Infraestructura.Datos;
using SGE.Infraestructura.Tramites;
using SGE.Infraestructura.Expedientes;
using SGE.Infraestructura.Autorizaciones;
using SGE.Infraestructura.Usuarios;
using SGE.Infraestructura.UDT;

namespace SGE.Infraestructura.Extensiones;

public static class InfreaestructuraExtension
{
    public static IServiceCollection AddInfraestructura(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SgeDb");
        //registro GestionContext en el contenedor y lo configuro para que use sqlite y con la cadena "SgeDb"
        services.AddDbContext<SgeContext>(opciones => opciones.UseSqlite(connectionString));

        //cuando se requiera alguna de estas interfaces les devolvemos las implementaciones
        services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
        services.AddScoped<IExpedienteRepository, ExpedienteRepository>();
        services.AddScoped<ITramiteRepository, TramiteRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IAutorizacionService, AutorizacionService>();

        return services;
    }
}