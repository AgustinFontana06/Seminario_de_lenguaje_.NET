using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Excepciones;
namespace SGE.Aplicacion.Usuarios.Admin;


public class EliminarUsuarioUseCase(IUsuarioRepository usuarioRepository, IUnidadDeTrabajo udt)
{
    public EliminarUsuarioResponse Ejecutar(EliminarUsuarioRequest request)
    {
        var ejecutor = usuarioRepository.ObtenerPorId(request.UsuarioEjecutorId);

        if(ejecutor == null || !ejecutor.EsAdministrador)
        {
            throw new AutorizacionException("Se requieren permisos de admnistrador");
        }

        var usuario = usuarioRepository.ObtenerPorId(request.id);

        if(usuario == null)
        {
            throw new EntidadNoEncontradaException("No se encontro el usuario a eliminar");
        }

        if(request.id == request.UsuarioEjecutorId)
        {
            throw new Exception("un administrador no se puede eliminar a si mismo");
        }

        usuarioRepository.Eliminar(request.id);
        udt.Guardar();
        return new EliminarUsuarioResponse("Usuario eliminado con exito");
    }
}

