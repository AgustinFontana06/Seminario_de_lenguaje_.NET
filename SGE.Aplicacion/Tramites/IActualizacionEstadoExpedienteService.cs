namespace SGE.Aplicacion.Tramites;
using SGE.Dominio.Expedientes;


public interface IActualizacionEstadoExpedienteService
{
    public void ActualizacionEstado(Guid expedienteId, Guid usuarioId);
}