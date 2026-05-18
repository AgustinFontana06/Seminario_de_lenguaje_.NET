namespace SGE.Aplicacion.Tramites.Eliminar;
using SGE.Aplicacion.Autorizaciones;
using SGE.Aplicacion.Comun;
using SGE.Dominio.Tramites;

public class EliminarTramiteUseCase(ITramiteRepository repositorioTramite, IActualizacionEstadoExpedienteService actualizacion, IAutorizacionService autorizacion)
{
    public EliminarTramiteResponse Ejecutar(EliminarTramiteRequest request)
    {
        if(!autorizacion.PoseeElPermiso(request.idUsuario, Permiso.TramiteBaja))
        {
            throw new AutorizacionException("No tienes permiso para dar de baja el tramite.");
        }

        Tramite? tramite = repositorioTramite.ObtenerPorId(request.idTramite);

        if(tramite == null)
        {
            throw new EntidadNoEncontradaException($"No se encontro el tramite con el id {request.idTramite}");
        }

        repositorioTramite.Eliminar(request.idTramite);

        //actualizo estado del expediente
        actualizacion.ActualizacionEstado(tramite.ExpedienteId, request.idUsuario);
    
        return new EliminarTramiteResponse(request.idTramite);
    }
}