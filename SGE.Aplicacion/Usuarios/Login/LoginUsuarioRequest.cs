using SGE.Dominio.Usuario;

namespace SGE.Aplicacion.Usuarios;


public record LoginUsuarioRequest(string email, string password){}
