namespace SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Permisos;

public interface IAutorizacionService
{
    bool PoseeElPermiso(Guid idUsuario, Permiso permisoRequerido);
}