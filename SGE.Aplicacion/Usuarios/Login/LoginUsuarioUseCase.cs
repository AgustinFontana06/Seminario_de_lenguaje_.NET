using System.Security.Cryptography;
using System.Text;
using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Excepciones;
using SGE.Dominio.Usuario;

namespace SGE.Aplicacion.Usuarios.Login;

public class LoginUsuarioUseCase(IUsuarioRepository usuarioRepository, IUnidadDeTrabajo udt)
{
    public LoginUsuarioResponse Ejecutar( LoginUsuarioRequest request)
    {
        var usuarioBuscado = usuarioRepository.obtenerPorEmail(request.email);
        if (usuarioBuscado == null)
        {
            throw new EntidadNoEncontradaException("El usuario con el que se esta queriendo loguear no existe.");
        }

        //hashear en infraestructura
        var bytes = Encoding.UTF8.GetBytes(request.password);
        var hashBytes = SHA256.HashData(bytes);
        var passwordRequestHash = Convert.ToHexString(hashBytes); // o Convert.ToBase64String(hashBytes) dependiendo de cómo lo guardes

        // Asumiendo que usuarioBuscado tiene una propiedad Password donde se almacena el hash
        if(usuarioBuscado.ContrasenaHash != passwordRequestHash)
        {
           throw new AutorizacionException($"las credenciales utilizadas para el email {request.email} no son validas.");
        }

        udt.GuardarCambios();

        return new LoginUsuarioResponse(usuarioBuscado.Id);
    }
}