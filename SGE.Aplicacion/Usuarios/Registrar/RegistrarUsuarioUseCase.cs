namespace SGE.Aplicacion.Usuarios.Registrar;

using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Excepciones;
using SGE.Dominio.Usuarios;
using System.Security.Cryptography;
using System.Text;

public class RegistrarUsuarioUseCase(IUnidadDeTrabajo udt, IUsuarioRepository usuarioRepository)
{
    public RegistrarUsuarioResponse Ejecutar(RegistrarUsuarioRequest request)
    {
        //1.Validar que el usuario no existe
        if(usuarioRepository.ExisteUsuarioPorMail(request.email))
        {
            throw new EntidadNoEncontradaException("El usuario ya existe");
        }

        //2.Crear el usuario
        var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(request.password)));
        var usuario = new Usuario(request.nombre, DireccionEmail.Parse(request.email), hash);
        usuarioRepository.AgregarUsuario(usuario);
        udt.GuardarCambios();
        return new RegistrarUsuarioResponse(usuario.Id);
    }
}