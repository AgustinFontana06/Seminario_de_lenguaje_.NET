using SGE.Dominio;
namespace SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Usuario;
public interface ITokenProvider
{
 string GenerarToken(Usuario usuario);
}
