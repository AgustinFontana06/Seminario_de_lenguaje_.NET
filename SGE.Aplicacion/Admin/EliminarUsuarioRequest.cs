namespace SGE.Aplicacion.Admin;

public record class EliminarUsuarioRequest(Guid UsuarioEjecutorId, Guid id);