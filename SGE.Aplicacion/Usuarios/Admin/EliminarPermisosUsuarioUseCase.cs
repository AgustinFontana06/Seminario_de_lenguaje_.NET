using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Excepciones;
using SGE.Dominio.Permisos;
namespace SGE.Aplicacion.Usuarios.Admin;

public class EliminarPermisosUsuarioUseCase(IUsuarioRepository usuarioRepository)
{
    public EliminarPermisosUsuarioResponse Ejecutar(EliminarPermisosUsuarioRequest request)
    {
        var ejecutor = usuarioRepository.ObtenerPorId(request.UsuarioEjecutorId);

        if(ejecutor == null || !ejecutor.EsAdministrador)
        {
            throw new AutorizacionException("Se requieren permisos de admnistrador");
        }

        var usuario = usuarioRepository.ObtenerPorId(request.id);

        if(usuario == null)
        {
            throw new EntidadNoEncontradaException("No se encontro el usuario");
        }

        var permisosAEliminar = request.permisos.Intersect(usuario.ListaDePermisos).ToList();

        if (permisosAEliminar.Any())
        {
            foreach (var permiso in permisosAEliminar)
            {
                usuario.ListaDePermisos.Remove(permiso);
            }

            usuarioRepository.EliminarPermiso(usuario);
        }

        return new EliminarPermisosUsuarioResponse("permisos removidos correctamente");
        
    }
}