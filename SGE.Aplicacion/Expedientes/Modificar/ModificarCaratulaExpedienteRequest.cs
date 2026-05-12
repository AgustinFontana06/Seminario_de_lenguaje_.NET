namespace SGE.Aplicacion.Expedientes.Modificar;
using SGE.Dominio.Expedientes;

public record class ModificarCaratulaExpedienteRequest(string text, Guid id){}