using SGE.Dominio.Usuario;

namespace SGE.Aplicacion.Usuarios;


public record LoginUsuarioRequest(DireccionEmail email, string password){}
