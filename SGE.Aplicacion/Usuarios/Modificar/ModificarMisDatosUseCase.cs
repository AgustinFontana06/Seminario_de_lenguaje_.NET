using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Excepciones;
using SGE.Dominio.Usuarios;
using System.Security.Cryptography;
using System.Text;


namespace SGE.Aplicacion.Usuarios.Modificar;
public class ModificarMisDatosUseCase(IUsuarioRepository usuarioRepository, IUnidadDeTrabajo udt)
{
    public ModificarMisDatosResponse Ejecutar(ModificarMisDatosRequest request, Guid usuarioAutenticadoId)
    {
        var usuario = usuarioRepository.ObtenerPorId(usuarioAutenticadoId);
        if (usuario == null)
        {
            throw new EntidadNoEncontradaException("Usuario no encontrado");
        }

        usuario.ModificarDatos(request.nombre, DireccionEmail.Parse(request.email), Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(request.contraseña))));

        usuarioRepository.Modificar(usuario);
        udt.GuardarCambios();
        return new ModificarMisDatosResponse(usuario.Id);
    }
}