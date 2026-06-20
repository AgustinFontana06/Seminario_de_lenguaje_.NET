using SGE.Dominio;
using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Excepciones;
namespace SGE.Aplicacion.Admin;

public class ListarUsuariosUseCase(IUsuarioRepository usuarioRepository)
{
    public ListarUsuariosResponse Ejecutar(ListarUsuariosRequest request)
    {
        var ejecutor = usuarioRepository.ObtenerPorId(request.UsuarioEjecutorId);

        if(ejecutor == null || !ejecutor.EsAdministrador)
        {
            throw new AutorizacionException("Se requieren permisos de admnistrador");
        }

        var listaUsuarios = usuarioRepository.ObtenerTodos();

        var usuariosMapeados = listaUsuarios.Select(u => new UsuarioItemDTO(
            u.Id,
            u.Nombre,
            u.Email.ToString(),
            u.EsAdministrador,
            u.ListaDePermisos
        ));

        return new ListarUsuariosResponse(usuariosMapeados);
    }
}