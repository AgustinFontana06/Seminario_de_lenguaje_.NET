namespace SGE.Aplicacion.Tramites.Agregar;

using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Tramites;
using SGE.Dominio.Permisos;

public class AgregarTramiteUseCase(ITramiteRepository repositorio, IActualizacionEstadoExpedienteService actualizacion, IAutorizacionService autorizacion, IUnidadDeTrabajo udt)
{
    public AgregarTramiteResponse Ejecutar(AgregarTramiteRequest request, Guid idUsuario)
    {
        if(!autorizacion.PoseeElPermiso(idUsuario, Permiso.TramiteAlta))
        {
            throw new AutorizacionException("No tienes permiso para dar de alta el tramite.");
        }
        
        var contenido = new ContenidoTramite(request.contenidoText);
        var tramite = new Tramite(request.expedienteId, contenido,idUsuario);
        //llamamos al servicio por si hay que cambiar automaticamente el state del expediente.
        repositorio.Agregar(tramite);
        udt.GuardarCambios(); //se guarda antes porque en actualizacionEstado se busca en la base de datos en expediente a actualizar
        actualizacion.ActualizacionEstado(request.expedienteId, idUsuario);
        udt.GuardarCambios();
        return new AgregarTramiteResponse(tramite.Id, idUsuario);
    }
}