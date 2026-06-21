using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios;


public record LoginUsuarioRequest(string email, string password){}
