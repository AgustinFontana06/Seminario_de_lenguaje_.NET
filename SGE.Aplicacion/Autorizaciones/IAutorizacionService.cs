namespace SGE.Aplicacion.Autorizaciones;

public interface IAutorizacionService
{
    bool PoseeElPermiso(Guid idUsuario, Permiso permisoRequerido);
}