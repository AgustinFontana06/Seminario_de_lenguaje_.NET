namespace SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Usuarios;

public interface IUsuarioRepository : IRepository<Usuario>
{
    bool ExisteUsuario(Guid id);
    Usuario? obtenerPorEmail(DireccionEmail email);
    void AgregarUsuario(Usuario usuario);
}
