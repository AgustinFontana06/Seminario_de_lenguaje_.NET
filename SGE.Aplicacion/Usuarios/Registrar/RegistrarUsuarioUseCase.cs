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

        //2.Crear el usuario
        var usuario = new Usuario(Guid.NewGuid(), request.nombre, request.email, request.password);
        usuarioRepository.Agregar(usuario);
        unidadDeTrabajo.GuardarCambios();
        return new RegistrarUsuarioResponse(usuario.Id);
    }
}