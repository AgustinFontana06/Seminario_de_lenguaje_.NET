using SGE.Aplicacion.Usuarios.Login;
using SGE.Aplicacion.Usuarios.Modificar;
using SGE.Aplicacion.Usuarios.Admin;
using SGE.Aplicacion.Usuarios.Registrar;
using SGE.Dominio.Permisos;
using System.Security.Claims;

namespace SGE.WebApi.Endpoints;

public static class UsuariosEndpoints
{
    public static IEndpointRouteBuilder MapUsuariosEndpoints(this IEndpointRouteBuilder app)
    {
        //---- METODO POST (LOGIN) ----
        app.MapPost("/api/login", (LoginUsuarioRequest request, LoginUsuarioUseCase useCase) =>
        {
            var response = useCase.Ejecutar(request);
            return Results.Ok(response);
        });

        //---- METODO POST (REGISTER) ----
        app.MapPost("/api/register", (RegistrarUsuarioRequest request, RegistrarUsuarioUseCase useCase) =>
        {
            var response = useCase.Ejecutar(request);
            return Results.Ok(response);
        });

        //---- GRUPO USUARIOS ----
        var grupo = app.MapGroup("/usuarios").WithTags("Usuarios");

        //---- METODO PUT (MODIFICAR MIS DATOS) ----
        grupo.MapPut("/me", (ModificarMisDatosRequest request, ClaimsPrincipal user, ModificarMisDatosUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);
            var response = useCase.Ejecutar(request, idUsuario);
            return Results.Ok(response);
        }).RequireAuthorization();

        //---- METODOS ADMIN ----
        var admin = grupo.MapGroup("/admin").WithTags("Usuarios Admin").RequireAuthorization();

        // GET /usuarios/admin/listar
        admin.MapGet("/listar", (ClaimsPrincipal user, ListarUsuariosUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);
            var response = useCase.Ejecutar(new ListarUsuariosRequest(idUsuario));
            return Results.Ok(response);
        });

        // DELETE /usuarios/admin/eliminar/{id}
        admin.MapDelete("/eliminar/{id}", (Guid id, ClaimsPrincipal user, EliminarUsuarioUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);
            var response = useCase.Ejecutar(new EliminarUsuarioRequest(idUsuario, id));
            return Results.Ok(response);
        });

        // PUT /usuarios/admin/agregar-permisos/{id}
        admin.MapPut("/agregar-permisos/{id}", (Guid id, AgregarPermisosRequest request, ClaimsPrincipal user, AgregarPermisosUsuarioUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);
            var response = useCase.Ejecutar(new AgregarPermisosUsuarioRequest(idUsuario, id, request.permisos));
            return Results.Ok(response);
        });

        // PUT /usuarios/admin/eliminar-permisos/{id}
        admin.MapPut("/eliminar-permisos/{id}", (Guid id, EliminarPermisosRequest request, ClaimsPrincipal user, EliminarPermisosUsuarioUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);
            var response = useCase.Ejecutar(new EliminarPermisosUsuarioRequest(idUsuario, id, request.permisos));
            return Results.Ok(response);
        });

        return app;
    }
}

// DTOs locales para los endpoints admin (request del body)
public record AgregarPermisosRequest(IEnumerable<Permiso> permisos);
public record EliminarPermisosRequest(IEnumerable<Permiso> permisos);
