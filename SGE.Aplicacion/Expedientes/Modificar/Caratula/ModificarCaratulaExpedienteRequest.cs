namespace SGE.Aplicacion.Expedientes.Modificar;
using SGE.Dominio.Expedientes;

public record class ModificarCaratulaExpedienteRequest(Guid idExpediente, string texto, Guid idUsuario){}