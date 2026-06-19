namespace SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Usuario;

public interface IUsuarioRepository : IRepository<Usuario>
{
    bool ExisteUsuario(string email);
    Usuario? obtenerPorEmail(string email);
}
