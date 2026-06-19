namespace Aplicacion.Usuarios.Listar;

public record class UsuarioDto(Guid Id, string Nombre, string Email);

public record ListarUsuariosResponse(IEnumerable<UsuarioDto> Usuarios){}