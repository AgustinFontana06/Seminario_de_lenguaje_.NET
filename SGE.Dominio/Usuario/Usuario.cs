namespace SGE.Dominio.Usuario;
using SGE.Dominio.Comun;
using SGE.Dominio.Permiso;

public class Usuario
{
    public Guid Id {get; private set;}
    public String Nombre {get; private set;}
    public String CorreoElectronico {get; private set;}
    public String ContrasenaHash {get; private set;}
    public bool EsAdministrador {get; private set;}
    public IEnumerable <Permiso> ListaDePermisos {get; private set;}


    public Usuario(Guid idUsuario,String nombre,String email,String password)
    {
    
       if (string.IsNullOrWhiteSpace(nombre))
         throw new DominioException("El nombre del usuario no puede estar vacío.");

       if (email == null)
            throw new DominioException("El email del usuario es obligatorio.");

       if (string.IsNullOrWhiteSpace(password))
            throw new DominioException("El password del usuario no puede estar vacío."); 
       if(idUsuario == Guid.Empty)
            throw new DominioException("El id no puede estar vacio");
        
        Id=idUsuario;
        Nombre=nombre;
        CorreoElectronico= email;
        ContrasenaHash=password;
        EsAdministrador= false;

        ListaDePermisos = Enumerable.Empty<Permiso>(); // raro pero supustamente es mejor en terminos de memoria;
    }
}