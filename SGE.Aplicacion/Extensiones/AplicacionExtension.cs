using SGE.Aplicacion.Expedientes.Agregar;
using SGE.Aplicacion.Expedientes.Eliminar;
using SGE.Aplicacion.Expedientes.Modificar;
using SGE.Aplicacion.Expedientes.ObtenerPorId;
using SGE.Aplicacion.Expedientes.ObtenerTodos;
using SGE.Aplicacion.Tramites.Agregar;
using SGE.Aplicacion.Tramites.Eliminar;
using SGE.Aplicacion.Tramites.Modificar;
using SGE.Aplicacion.Tramites.ObtenerExpedientePorId;
using SGE.Aplicacion.Tramites.ObtenerPorId;
using SGE.Aplicacion.Usuarios.Registrar;
using Microsoft.Extensions.DependencyInjection;

namespace SGE.Aplicacion.Extensiones;

public static class AplicacionExtension()
{
    public static IServiceCollection AddAplicacion(this IServiceCollection services)
    {
        //se crea una sola instancia por peticion, es decir se reutiliza cada scoped por use case requerido
        services.addScoped<AgregarExpedienteUseCase>();
        services.addScoped<EliminarExpedienteUseCase>();
        services.addScoped<ModificarCaratulaExpedienteUseCase>();
        services.addScoped<CambiarEstadoUseCase>();
        services.addScoped<ObtenerPorIdUseCase>();
        services.addScoped<ObtenerTodosUseCase>();

        services.addScoped<AgregarTramiteUseCase>();
        services.addScoped<EliminarTramiteUseCase>();
        services.addScoped<ModificarTramiteUseCase>();

        services.addScoped<Tramites.ObtenerPorId.ObtenerPorIdUseCase>();
        services.addScoped<ObtenerPorExpedienteIdUseCase>();

        services.addScoped<RegistrarUsuarioUseCase>();

        return services;
    }
}