namespace SGE.Infraestructura.Usuarios;

using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Usuarios;
using SGE.Infraestructura.Datos;

public class UsuarioRepository(GestionContext context) : Repository<Usuario>(context), IUsuarioRepository // falta implementar cosas
{


    public Usuario? obtenerPorEmail(DireccionEmail email)
    {
        return _dbSet.FirstOrDefault(u => u.Email == email);
    }

    public void AgregarUsuario(Usuario usuario)
    {
        _dbSet.Add(usuario);
    }

    public bool ExisteUsuarioPorMail(string email)
    {
        var partes = email.Split('@');
        if(partes.Length != 2) return false;
        var dir = new DireccionEmail(partes[0], partes[1]);
        return _dbSet.Any(u => u.Email == dir);
    }

    //marcar al usuario como modificado
    public void AgregarPermiso(Usuario u)
    {
        _dbSet.Update(u);
    }
    
    public void EliminarPermiso(Usuario u)
    {
        _dbSet.Update(u);
    }
}