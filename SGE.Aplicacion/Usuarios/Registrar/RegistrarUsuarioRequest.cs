namespace SGE.Aplicacion.Usuarios.Registrar;
using SGE.Dominio.Permisos;
using SGE.Dominio.Usuarios;

public record RegistrarUsuarioRequest(string nombre, string email, string password){}