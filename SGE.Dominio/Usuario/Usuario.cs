namespace SGE.Dominio.Usuario;
using SGE.Dominio.Excepciones;
using SGE.Dominio.Permisos;
using SGE.Dominio.Abstracciones;

public class Usuario: Entidad
{
    public String Nombre {get; private set;} ="";
    public DireccionEmail Email {get; private set;}
    public String ContrasenaHash {get; private set;}
    public bool EsAdministrador {get; private set;} = false; // por defecto no es administrador.
    public IEnumerable <Permiso> ListaDePermisos {get; private set;}


    public Usuario(String nombre, DireccionEmail email,String password)
    {
          if (string.IsNullOrWhiteSpace(nombre))
               throw new DominioException("El nombre del usuario no puede estar vacío.");

          if (email == null)
               throw new DominioException("El email del usuario es obligatorio.");
          
          if (string.IsNullOrWhiteSpace(password))
               throw new DominioException("El password del usuario no puede estar vacío.");


          Nombre=nombre;
          Email= email;
          ContrasenaHash=password;
          EsAdministrador = false;
          ListaDePermisos = Enumerable.Empty<Permiso>(); // raro pero supustamente es mejor en terminos de memoria;
    }

    public void setAdmin() => EsAdministrador = true;
    
    protected Usuario() { }
}