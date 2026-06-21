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
using SGE.Aplicacion.Usuarios.Login;
using SGE.Aplicacion.Usuarios.Modificar;
using SGE.Aplicacion.Admin;
using Microsoft.Extensions.DependencyInjection;

namespace SGE.Aplicacion.Extensiones;

public static class AplicacionExtension
{
    public static IServiceCollection AddAplicacion(this IServiceCollection services)
    {  
        //se crea una sola instancia por peticion, es decir se reutiliza cada scoped por use case requerido
        //expedientes
        services.AddScoped<AgregarExpedienteUseCase>();
        services.AddScoped<EliminarExpedienteUseCase>();
        services.AddScoped<ModificarCaratulaExpedienteUseCase>();
        services.AddScoped<CambiarEstadoUseCase>();
        services.AddScoped<SGE.Aplicacion.Expedientes.ObtenerPorId.ObtenerPorIdUseCase>();
        services.AddScoped<ObtenerTodosUseCase>();

        //tramites
        services.AddScoped<AgregarTramiteUseCase>();
        services.AddScoped<EliminarTramiteUseCase>();
        services.AddScoped<ModificarTramiteUseCase>();
        services.AddScoped<SGE.Aplicacion.Tramites.ObtenerPorId.ObtenerPorIdUseCase>();
        services.AddScoped<ObtenerPorExpedienteIdUseCase>();

        //usuarios
        services.AddScoped<RegistrarUsuarioUseCase>();
        services.AddScoped<LoginUsuarioUseCase>();
        services.AddScoped<ModificarMisDatosUseCase>();

        //admin
        services.AddScoped<ListarUsuariosUseCase>();
        services.AddScoped<EliminarUsuarioUseCase>();
        services.AddScoped<AgregarPermisosUsuarioUseCase>();
        services.AddScoped<EliminarPermisosUsuarioUseCase>();

        return services;
    }
}