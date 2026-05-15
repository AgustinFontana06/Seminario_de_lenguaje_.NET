namespace SGE.Aplicacion.Tramites.Agregar;

using SGE.Aplicacion.Autorizaciones;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Tramites;

public class AgregarTramiteUseCase(ITramiteRepository repositorio, IActualizacionEstadoExpedienteService actualizacion, IAutorizacionService autorizacion)
{
    public AgregarTramiteResponse Ejecutar(AgregarTramiteRequest request)
    {
        if(!autorizacion.PoseeElPermiso(request.idUsuario, Permiso.TramiteAlta))
        {
            throw new AutorizacionException("No tienes permiso para dar de alta el tramite.");
        }
        
        var contenido = new ContenidoTramite(request.contenidoText);
        var tramite = new Tramite(request.expedienteId, contenido,request.idUsuario);
        //llamamos al servicio por si hay que cambiar automaticamente el state del expediente.
        repositorio.Agregar(tramite);
        actualizacion.ActualizacionEstado(request.expedienteId, request.idUsuario);
        return new AgregarTramiteResponse(tramite.Id, request.idUsuario);
    }
}