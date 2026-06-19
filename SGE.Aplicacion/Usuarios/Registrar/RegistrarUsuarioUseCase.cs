namespace SGE.Aplicacion.Usuarios.Registrar;

using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Excepciones;
using SGE.Dominio.Usuario;

public class RegistrarUsuarioUseCase(IUnidadDeTrabajo unidadDeTrabajo, IUsuarioRepository usuarioRepository)
{
    public RegistrarUsuarioResponse Ejecutar(RegistrarUsuarioRequest request)
    {
        //1.Validar que el usuario no existe
        if(usuarioRepository.ExisteUsuario(request.email))
        {
            throw new EntidadNoEncontradaException("El usuario ya existe");
        }
        var partesEmail = request.email.Split('@');
        if (partesEmail.Length != 2)
        {
            // Lanzamos DominioException para que nuestro futuro filtro devuelva un HTTP 400
            throw new ApplicationException("El formato del email no es válido.");
        }

        //2.Crear el usuario
        var direccionEmail = new DireccionEmail(partesEmail[0], partesEmail[1]);
        var usuario = new Usuario(request.nombre, direccionEmail,  request.password);
        usuarioRepository.Agregar(usuario);
        unidadDeTrabajo.GuardarCambios();
        return new RegistrarUsuarioResponse(usuario.Id);
    }
}