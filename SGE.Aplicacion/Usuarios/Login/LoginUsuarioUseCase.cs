using System.Security.Cryptography;
using System.Text;
using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Excepciones;
using SGE.Dominio.Usuario;
using SGE.Dominio.Excepciones;
namespace SGE.Aplicacion.Usuarios.Login;

public class LoginUsuarioUseCase(IUnidadDeTrabajo unidadDeTrabajo, IUsuarioRepository usuarioRepository,ITokenProvider tokenProvider)
{
    public LoginUsuarioResponse Ejecutar( LoginUsuarioRequest request)
    {
        DireccionEmail emailIngresado;
         // 1. Delegamos la validación al Dominio intentando crear el Value Object
        try
        {
            emailIngresado = DireccionEmail.Parse(request.email);
        }
        catch (DominioException) // Atrapamos la regla de dominio rota
        {
            throw new AutorizacionException("El formato del email o la contraseña son incorrectos.");
        }
 //
        var usuarioBuscado = usuarioRepository.obtenerPorEmail(emailIngresado);
        if (usuarioBuscado == null)
        {
            throw new EntidadNoEncontradaException("El usuario con el que se esta queriendo loguear no existe.");
        }
        var bytes = Encoding.UTF8.GetBytes(request.password);
        var hashBytes = SHA256.HashData(bytes);
        var passwordRequestHash = Convert.ToHexString(hashBytes); // o Convert.ToBase64String(hashBytes) dependiendo de cómo lo guardes

        // Asumiendo que usuarioBuscado tiene una propiedad Password donde se almacena el hash
        if(usuarioBuscado.ContrasenaHash != passwordRequestHash)
        {
           throw new AutorizacionException($"las credenciales utilizadas para el email {request.email} no son validas.");
        }
        // 4. Delegamos la creación del token a la infraestructura
        var token = tokenProvider.GenerarToken(usuarioBuscado);
        return new LoginUsuarioResponse(token);
    }
}