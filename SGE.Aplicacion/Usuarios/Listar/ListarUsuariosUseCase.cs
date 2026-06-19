using SGE.Aplicacion.Abstracciones;

namespace Aplicacion.Usuarios.Listar;

public class ListarUsuariosUseCase(IUsuarioRepository usuarioRepository)
{
    public ListarUsuariosResponse Ejecutar( ListarUsuariosRequest request)
    {
        // 1. Obtenemos las entidades vivas
        var usuarios = usuarioRepository.ObtenerTodos();
        
        
    }
}