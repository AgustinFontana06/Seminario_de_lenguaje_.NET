namespace SGE.Aplicacion.Usuarios.Registrar;
using SGE.Dominio.Permisos;

public record RegistrarUsuarioRequest(string nombre, string email, string password, Permiso permiso){}