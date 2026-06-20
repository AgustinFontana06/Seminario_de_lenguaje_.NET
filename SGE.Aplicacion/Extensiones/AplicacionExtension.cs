using SGE.Aplicacion.Expedientes.Agregar;
using SGE.Aplicacion.Expedientes.Eliminar;
using SGE.Aplicacion.Expedientes.Modificar;
using SGE.Aplicacion.Expedientes.ObtenerTodos;
using SGE.Aplicacion.Tramites.Agregar;
using SGE.Aplicacion.Tramites.Eliminar;
using SGE.Aplicacion.Tramites.Modificar;
using SGE.Aplicacion.Tramites.ObtenerExpedientePorId;
using SGE.Aplicacion.Usuarios.Registrar;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ObtenerExpedientePorIdUseCase =
    SGE.Aplicacion.Expedientes.ObtenerPorId.ObtenerPorIdUseCase;

using ObtenerTramitePorIdUseCase =
    SGE.Aplicacion.Tramites.ObtenerPorId.ObtenerPorIdUseCase;
namespace SGE.Aplicacion.Extensiones;

public static class AplicacionExtension
{
    public static IServiceCollection AddAplicacion(this IServiceCollection services,IConfiguration configuration)
    {
        //se crea una sola instancia por peticion, es decir se reutiliza cada scoped por use case requerido
        services.AddScoped<AgregarExpedienteUseCase>();
        services.AddScoped<EliminarExpedienteUseCase>();
        services.AddScoped<ModificarCaratulaExpedienteUseCase>();
        services.AddScoped<CambiarEstadoUseCase>();
        services.AddScoped<ObtenerExpedientePorIdUseCase>();

        services.AddScoped<ObtenerTodosUseCase>();

        services.AddScoped<AgregarTramiteUseCase>();
        services.AddScoped<EliminarTramiteUseCase>();
        services.AddScoped<ModificarTramiteUseCase>();

        services.AddScoped<ObtenerTramitePorIdUseCase>();
        services.AddScoped<ObtenerPorExpedienteIdUseCase>();

        services.AddScoped<RegistrarUsuarioUseCase>();

        return services;
    }
}