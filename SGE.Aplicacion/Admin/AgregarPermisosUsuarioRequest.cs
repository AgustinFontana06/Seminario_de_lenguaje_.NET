using SGE.Dominio.Permisos;
namespace SGE.Aplicacion.Admin;

public record class AgregarPermisosUsuarioRequest(Guid UsuarioEjecutorId, Guid id, IEnumerable<Permiso> permisos);