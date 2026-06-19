namespace SGE.Aplicacion.Usuarios.Registrar;

using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Excepciones;
using SGE.Dominio.Usuario;

public class RegistrarUsuarioUseCase(IUnidadDeTrabajo udt, IUsuarioRepository usuarioRepository)
{
    public RegistrarUsuarioResponse Ejecutar(RegistrarUsuarioRequest request)
    {
        //1.Validar que el usuario no existe
        if(usuarioRepository.ExisteUsuario(request.id))
        {
            throw new EntidadNoEncontradaException("El usuario ya existe");
        }

        //2.Crear el usuario
        var usuario = new Usuario(request.nombre, request.email, request.password);
        usuarioRepository.AgregarUsuario(usuario);
        udt.GuardarCambios();
        return new RegistrarUsuarioResponse(usuario.Id);
    }
}