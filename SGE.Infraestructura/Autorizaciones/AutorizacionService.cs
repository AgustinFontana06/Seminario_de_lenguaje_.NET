namespace SGE.Infraestructura.Autorizaciones;
using SGE.Dominio.Permisos;
using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Usuarios;
using SGE.Aplicacion.Excepciones;
using System.Runtime.CompilerServices;
public class AutorizacionService(IUsuarioRepository usuarioRepository) : IAutorizacionService
{
     public bool PoseeElPermiso(Guid id, Permiso permisoRequerido)
    {
        var usuario = usuarioRepository.ObtenerPorId(id);

        if (usuario == null) return false;

        if (usuario.EsAdministrador) return true;

        if (permisoRequerido == Permiso.TramiteBaja && usuario.ListaDePermisos.Contains(Permiso.ExpedienteBaja))
        {
            return true;
        }

        IEnumerable<Permiso> permisos = usuario.ListaDePermisos;
        if (permisos.Contains(permisoRequerido))
        {
            return true;
        }
        return false;
    }
}