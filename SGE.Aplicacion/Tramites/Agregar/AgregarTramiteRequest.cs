namespace SGE.Aplicacion.Tramites.Agregar;

public record AgregarTramiteRequest(string contenidoText, Guid idUsuario, Guid expedienteId);