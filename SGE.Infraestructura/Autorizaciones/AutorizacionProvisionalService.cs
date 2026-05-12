namespace SGE.Infraestructura.Autorizaciones;
using SGE.Aplicacion.Autorizaciones;
public class AutorizacionProvisionalService : IAutorizacionService
{
     public bool PoseeElPermiso(Guid idUsuario, Permiso permisoRequerido)
    {
        // en la segunda entrega si esto no se valida debe retornar una validacion excepcion
        return true;
    }
}