namespace SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Usuarios;
public interface ITokenProvider
{
 string GenerarToken(Usuario usuario);
}
