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
        grupo.MapGet("/{id}", (Guid id, ObtenerPorIdUseCase useCase) =>
        {
            var resultado = useCase.Ejecutar(new ObtenerPorIdRequest(id));
            return Results.Ok(resultado);
        });

        grupo.MapGet("/expediente/{expedienteId}", (Guid expedienteId, ObtenerPorExpedienteIdUseCase useCase) =>
        {
            var resultado = useCase.Ejecutar(new ObtenerPorExpedienteIdRequest(expedienteId));
            return Results.Ok(resultado);
        });

        //---- METODO POST ----
        grupo.MapPost("/", (AgregarTramiteRequest request, ClaimsPrincipal user, AgregarTramiteUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);
            var resultado = useCase.Ejecutar(new AgregarTramiteRequest(request.contenidoText, idUsuario, request.expedienteId));
            return Results.Created($"/tramites/{resultado.idTramite}", resultado);
        });

        //---- METODO DELETE ----
        grupo.MapDelete("/{id}", (Guid id, ClaimsPrincipal user, EliminarTramiteUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);
            var resultado = useCase.Ejecutar(new EliminarTramiteRequest(id, idUsuario));
            return Results.Ok(resultado);
        });

        //---- METODO PUT ----
        grupo.MapPut("/{id}", (Guid id, ModificarTramiteRequest request, ClaimsPrincipal user, ModificarTramiteUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);
            var resultado = useCase.Ejecutar(new ModificarTramiteRequest(id, request.texto, idUsuario));
            return Results.Ok(resultado);
        });

        return app;
    }
}
