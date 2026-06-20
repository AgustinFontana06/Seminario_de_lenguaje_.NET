using SGE.Dominio.Permisos;
namespace SGE.Aplicacion.Admin;

public record class EliminarPermisosUsuarioRequest(Guid UsuarioEjecutorId, Guid id, IEnumerable<Permiso> permisos);