namespace SGE.Infraestructura.Expedientes;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Expedientes;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;
using SGE.Aplicacion.Comun;

public class ActualizacionEstadoExpedienteService(
    IExpedienteRepository expedienteRepository,
    ITramiteRepository tramiteRepository) : IActualizacionEstadoExpedienteService
{
    public void ActualizacionEstado(Guid expedienteId, Guid idUsuario)
    {
        Expediente? expedienteBuscado = expedienteRepository.ObtenerPorId(expedienteId);
        if (expedienteBuscado == null)
            throw new EntidadNoEncontradaException($"El expediente con id {expedienteId} no fue encontrado.");

        IEnumerable<Tramite> tramitesAsociados = tramiteRepository.ObtenerPorExpedienteId(expedienteId);
        Tramite? ultimoTramite = tramitesAsociados.MaxBy(t => t.FechaCreacion);
        EtiquetaTramite? ultimaEtiqueta = ultimoTramite?.Etiqueta;

        bool cambio = expedienteBuscado.ActualizarEstado(ultimaEtiqueta, idUsuario);
        if (cambio)
            expedienteRepository.Modificar(expedienteBuscado);
    }
}