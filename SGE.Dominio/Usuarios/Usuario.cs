namespace SGE.Dominio.Usuarios;
using SGE.Dominio.Excepciones;
using SGE.Dominio.Permisos;
using SGE.Dominio.Abstracciones;

public class Usuario: Entidad
{
    public String Nombre {get; private set;} ="";
    public DireccionEmail Email {get; private set;}= new DireccionEmail("vacio","vacio.com");
    public String ContrasenaHash {get; private set;}="";
    public bool EsAdministrador {get; private set;} = false; // por defecto no es administrador.
    public List <Permiso> ListaDePermisos {get; private set;}=[];


    public Usuario(String nombre, DireccionEmail email,String HashedPassword)
    {
          if (string.IsNullOrWhiteSpace(nombre))
               throw new DominioException("El nombre del usuario no puede estar vacío.");

          if (email == null)
               throw new DominioException("El email del usuario es obligatorio.");
          
          if (string.IsNullOrWhiteSpace(HashedPassword))
               throw new DominioException("El password del usuario no puede estar vacío.");


          Nombre=nombre;
          Email= email;
          ContrasenaHash=HashedPassword;
          EsAdministrador = false;
          ListaDePermisos = [] ;
    }

     public void AgregarPermiso(Permiso permiso)
     {
          if (!ListaDePermisos.Contains(permiso))
          {
               ListaDePermisos.Add(permiso);
          }
     }

     public void EliminarPermiso(Permiso permiso)
     {
          ListaDePermisos.Remove(permiso);
     }

     public void ModificarDatos(string nombre, DireccionEmail email, string contrasenaHash)
     {
          if (string.IsNullOrWhiteSpace(nombre))
               throw new DominioException("El nombre no puede estar vacío.");
          Nombre = nombre;
          Email = email;
          ContrasenaHash = contrasenaHash;
     }

    public void setAdmin() => EsAdministrador = true;
    
    protected Usuario() { }
}