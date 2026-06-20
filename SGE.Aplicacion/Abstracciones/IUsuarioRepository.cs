namespace SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Usuario;

public interface IUsuarioRepository : IRepository<Usuario>
{
<<<<<<< HEAD
    bool ExisteUsuario(string email);
    Usuario? obtenerPorEmail(DireccionEmail email);
=======
    bool ExisteUsuario(Guid id);
    Usuario? obtenerPorEmail(DireccionEmail email);
    void AgregarUsuario(Usuario usuario);
>>>>>>> 335a4f7036ec1e76831cd6e7e01182a8663376e6
}
