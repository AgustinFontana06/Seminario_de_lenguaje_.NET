using SGE.Dominio.Permisos;

namespace SGE.Aplicacion.Usuarios.Admin;

public record class UsuarioItemDTO(
    Guid Id,
    string Nombre,
    string Email,
    bool EsAdministrador,
    IEnumerable<Permiso> Permisos
);

// El DTO de salida global del Caso de Uso
public record class ListarUsuariosResponse(
    IEnumerable<UsuarioItemDTO> Usuarios
);