namespace SGE.Aplicacion.Tramites.Agregar;

using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Tramites;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Permisos;

public class AgregarTramiteUseCase(ITramiteRepository repositorio, IActualizacionEstadoExpedienteService actualizacion, IAutorizacionService autorizacion, IUnidadDeTrabajo udt, IExpedienteRepository repositorioExpediente)
{
    public AgregarTramiteResponse Ejecutar(AgregarTramiteRequest request, Guid idUsuario, Guid expedienteId)
    {
        if(!autorizacion.PoseeElPermiso(idUsuario, Permiso.TramiteAlta))
        {
            throw new AutorizacionException("No tienes permiso para dar de alta el tramite.");
        }
        
        Expediente? exp = repositorioExpediente.ObtenerPorId(expedienteId);
        if(exp == null)
        {
            throw new EntidadNoEncontradaException($"No se encontro el expediente con id {expedienteId}");
        }

        var contenido = new ContenidoTramite(request.contenidoText);
        var tramite = new Tramite(expedienteId, contenido,idUsuario);
        //llamamos al servicio por si hay que cambiar automaticamente el state del expediente.
        repositorio.Agregar(tramite);
        udt.Guardar(); //se guarda antes porque en actualizacionEstado se busca en la base de datos en expediente a actualizar
        actualizacion.ActualizacionEstado(expedienteId, idUsuario);
        udt.Guardar();
        return new AgregarTramiteResponse(tramite.Id, idUsuario);
    }
}