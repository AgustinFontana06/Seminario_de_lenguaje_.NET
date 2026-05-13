namespace SGE.Aplicacion.Expedientes.Modificar;
using SGE.Dominio.Expedientes;
public record class CambiarEstadoResponse(Guid id, EstadoExpediente nuevoEstado){}