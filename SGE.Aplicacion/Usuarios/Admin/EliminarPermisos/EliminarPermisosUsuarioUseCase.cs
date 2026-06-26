using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Excepciones;
using SGE.Dominio.Permisos;
using SGE.Dominio.Excepciones;
namespace SGE.Aplicacion.Usuarios.Admin;

public class EliminarPermisosUsuarioUseCase(IUsuarioRepository usuarioRepository, IUnidadDeTrabajo udt)
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

        foreach (var permiso in request.permisos)
        {   
            if (!Enum.IsDefined(typeof(Permiso), permiso))
                throw new DominioException($"El permiso '{permiso}' no es válido.");
        }

        var permisosAEliminar = request.permisos.Intersect(usuario.ListaDePermisos).ToList();

        if (permisosAEliminar.Any())
        {
            foreach (var permiso in permisosAEliminar)
            {
                usuario.EliminarPermiso(permiso);
            }

            usuarioRepository.EliminarPermiso(usuario);
        }

        udt.GuardarCambios();
        return new EliminarPermisosUsuarioResponse("permisos removidos correctamente");
        
    }
}