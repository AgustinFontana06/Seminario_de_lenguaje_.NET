using SGE.Dominio.Usuario;

namespace SGE.Aplicacion.Usuarios.Login;


public record LoginUsuarioRequest(string email, string password){}
