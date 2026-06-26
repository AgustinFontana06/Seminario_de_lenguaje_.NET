using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Excepciones;
using SGE.Dominio.Permisos;
using SGE.Dominio.Excepciones;
namespace SGE.Aplicacion.Usuarios.Admin;

public class AgregarPermisosUsuarioUseCase(IUsuarioRepository usuarioRepository, IUnidadDeTrabajo udt)
{
    public AgregarPermisosUsuarioResponse Ejecutar(AgregarPermisosUsuarioRequest request)
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

        var permisosFaltantes = request.permisos
            .Except(usuario.ListaDePermisos)
            .ToList();

        if (permisosFaltantes.Any())
        {
            foreach (var permiso in permisosFaltantes)
            {
                usuario.AgregarPermiso(permiso);
            }

            usuarioRepository.AgregarPermiso(usuario);
        }

        udt.GuardarCambios();
        return new AgregarPermisosUsuarioResponse("permisos agregados correctamente");
        
    }
}