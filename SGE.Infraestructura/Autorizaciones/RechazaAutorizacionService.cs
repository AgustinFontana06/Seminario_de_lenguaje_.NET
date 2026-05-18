namespace SGE.Infraestructura.Autorizaciones;
using SGE.Aplicacion.Autorizaciones;
public class RechazaAutorizacionService : IAutorizacionService
{
    public bool PoseeElPermiso(Guid idUsuario, Permiso permisoRequerido) => false;
}