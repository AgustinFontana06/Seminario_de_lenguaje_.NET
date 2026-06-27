using SGE.Aplicacion.Tramites.Agregar;
using SGE.Aplicacion.Tramites.Eliminar;
using SGE.Aplicacion.Tramites.Modificar;
using SGE.Aplicacion.Tramites.ObtenerPorId;
using SGE.Aplicacion.Tramites.ObtenerExpedientePorId;
using System.Security.Claims;

namespace SGE.WebApi.Endpoints;

public static class TramitesEndpoints
{
    public static IEndpointRouteBuilder MapTramitesEndpoints(this IEndpointRouteBuilder app)
    {
        var grupo = app.MapGroup("/tramites").WithTags("Tramites");

        //---- METODOS GET ----
        grupo.MapGet("/obtener-por/{id}", (Guid id, ObtenerPorIdUseCase useCase) =>
        {
            var resultado = useCase.Ejecutar(new ObtenerPorIdRequest(id));
            return Results.Ok(resultado);
        }).RequireAuthorization();

        grupo.MapGet("/obtener-por-expediente/{expedienteId}", (Guid expedienteId, ObtenerPorExpedienteIdUseCase useCase) =>
        {
            var resultado = useCase.Ejecutar(new ObtenerPorExpedienteIdRequest(expedienteId));
            return Results.Ok(resultado);
        }).RequireAuthorization();

        //---- METODO POST ----
        grupo.MapPost("/agregar-tramite/{expedienteId}", (Guid expedienteId, AgregarTramiteRequest request, ClaimsPrincipal user, AgregarTramiteUseCase useCase) =>
        {
            var idUsuario = Guid.Parse(user.FindFirst("ID")!.Value);
            var resultado = useCase.Ejecutar(request, idUsuario, expedienteId);
            return Results.Created($"/tramites/{resultado.idTramite}", resultado);
        }).RequireAuthorization();

        //---- METODO DELETE ----
        grupo.MapDelete("/eliminar-tramite/{id}", (Guid id, ClaimsPrincipal user, EliminarTramiteUseCase useCase) =>
        {
            var idUsuario = Guid.Parse(user.FindFirst("ID")!.Value);
            var resultado = useCase.Ejecutar(new EliminarTramiteRequest(id), idUsuario);
            return Results.Ok(resultado);
        }).RequireAuthorization();

        //---- METODO PUT ----
        grupo.MapPut("/modificar-tramite/{id}", (Guid id, ModificarTramiteRequest request, ClaimsPrincipal user, ModificarTramiteUseCase useCase) =>
        {
            var idUsuario = Guid.Parse(user.FindFirst("ID")!.Value);
            var resultado = useCase.Ejecutar(request, idUsuario, id);
            return Results.Ok(resultado);
        }).RequireAuthorization();

        return app;
    }
}
