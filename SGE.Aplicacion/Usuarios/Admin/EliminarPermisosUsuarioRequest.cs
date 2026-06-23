using SGE.Dominio.Permisos;
namespace SGE.Aplicacion.Usuarios.Admin;

public record class EliminarPermisosUsuarioRequest(Guid UsuarioEjecutorId, Guid id, IEnumerable<Permiso> permisos);