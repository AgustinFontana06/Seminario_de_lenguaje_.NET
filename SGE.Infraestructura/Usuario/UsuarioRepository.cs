namespace SGE.Infraestructura.Usuario;

using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Usuarios;
using SGE.Infraestructura.Datos;

public class UsuarioRepository(GestionContext context) : Repository<Usuario>(context), IUsuarioRepository
{

    public bool ExisteUsuario(Guid id)
    {
        return _dbSet.Any(u => u.Id == id);
    }

    public Usuario? obtenerPorEmail(DireccionEmail email)
    {
        return _dbSet.FirstOrDefault(u => u.Email == email);
    }

    public void AgregarUsuario(Usuario usuario)
    {
        _dbSet.Add(usuario);
    }
    

}