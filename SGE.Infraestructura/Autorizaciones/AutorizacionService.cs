namespace SGE.Infraestructura.Autorizaciones;
using SGE.Dominio.Permisos;
using SGE.Aplicacion.Autorizaciones;
using SGE.Dominio.Usuario;
public class AutorizacionService(GestionContext context) : IAutorizacionService
{
     public bool PoseeElPermiso(Guid idUsuario, Permiso permisoRequerido)
    {
        var usuario = context.Set<Usuario>().Find(idUsuario);
        if (usuario == null) return false;

        IEnumerable<Permiso> enum = usuario.listaDePermiso;

        return enum.a
    }
}