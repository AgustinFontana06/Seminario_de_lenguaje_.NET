namespace SGE.Aplicacion.Usuarios.Registrar;
using SGE.Dominio.Permisos;
using SGE.Dominio.Usuario;

public record RegistrarUsuarioRequest(string nombre, Guid id, DireccionEmail email, string password, Permiso permiso){}