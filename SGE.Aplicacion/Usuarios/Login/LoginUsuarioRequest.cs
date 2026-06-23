using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios.Login;


public record LoginUsuarioRequest(string email, string password){}
