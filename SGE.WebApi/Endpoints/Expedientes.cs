using SGE.Aplicacion.Expedientes.Agregar;
using SGE.Aplicacion.Expedientes.ObtenerTodos;
using SGE.Aplicacion.Expedientes.ObtenerPorId;
using SGE.Aplicacion.Expedientes.Eliminar;
using SGE.Aplicacion.Expedientes.Modificar;
using SGE.Dominio.Expedientes;
using System.Security.Claims;

namespace SGE.WebApi.Endpoints;

public static class ExpedientesEndpoints
{
    public static IEndpointRouteBuilder MapExpedientesEndpoints(this IEndpointRouteBuilder app)
    {
        var grupo = app.MapGroup("/expedientes").WithTags("Expedientes");

        // ----    METODOS GET ----
        grupo.MapGet("/", (ObtenerTodosUseCase useCase) =>
        {
            var resultado = useCase.Ejecutar(new ObtenerTodosRequest());
            return Results.Ok(resultado);
        });

        grupo.MapGet("/{id}", (Guid id, ObtenerPorIdUseCase useCase) =>
        {
            var resultado = useCase.Ejecutar(new ObtenerPorIdRequest(id));
            return Results.Ok(resultado);
        });

        //---- METODOS POST ----
        grupo.MapPost("/", (AgregarExpedienteRequest request, AgregarExpedienteUseCase useCase) =>
        {
            var resultado = useCase.Ejecutar(request);
            return Results.Created($"/expedientes/{resultado.id}", resultado);
        });

        //---- METODO DELETE ----
        grupo.MapDelete("/{id}", (Guid id, ClaimsPrincipal user, EliminarExpedienteUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);
            var resultado = useCase.Ejecutar(new EliminarExpedienteRequest(id, idUsuario));
            return Results.Ok(resultado);
        });

        //---- METODOS PUT ----
        grupo.MapPut("/{id}/caratula", (Guid id, ModificarCaratulaExpedienteRequest request, ClaimsPrincipal user, ModificarCaratulaExpedienteUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);
            var resultado = useCase.Ejecutar(new ModificarCaratulaExpedienteRequest(id, request.texto, idUsuario));
            return Results.Ok(resultado);
        });

        grupo.MapPut("/{id}/estado", (Guid id, CambiarEstadoRequest request, ClaimsPrincipal user, CambiarEstadoUseCase useCase) =>
        {
            var userIdString = user.FindFirst("ID")?.Value;
            var idUsuario = Guid.Parse(userIdString!);
            var resultado = useCase.Ejecutar(new CambiarEstadoRequest(id, request.estado, idUsuario));
            return Results.Ok(resultado);
        });

        return app;
    }
}