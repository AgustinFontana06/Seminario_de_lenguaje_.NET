namespace SGE.Aplicacion.Usuarios.Admin;

public record class EliminarUsuarioRequest(Guid UsuarioEjecutorId, Guid id);