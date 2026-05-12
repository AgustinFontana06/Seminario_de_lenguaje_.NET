namespace SGE.Infraestructura;
using SGE.Aplicacion;
public class AutorizacionProvisionalService : IAutorizacionService
{
     public bool PoseeElPermiso(Guid idUsuario, Permiso permisoRequerido)
    {
        return true;
        // en la segunda entrega si esto no se valida debe retornar una validacion excepcion
    }
}