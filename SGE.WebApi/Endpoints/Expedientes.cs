using SGE.Aplicacion.Expedientes.Agregar;
using SGE.Aplicacion.Expedientes.ObtenerTodos;
using SGE.Aplicacion.Expedientes.ObtenerPorId;

namespace SGE.WebApi.Endpoints;

public static class ExpedientesEndpoints
{
    public static IEndpointRouteBuilder MapExpedientesEndpoints(this IEndpointRouteBuilder app)
    {
        var grupo = app.MapGroup("/expedientes").WithTags("Expedientes");

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

        grupo.MapPost("/", (AgregarExpedienteRequest request, AgregarExpedienteUseCase useCase) =>
        {
            var resultado = useCase.Ejecutar(request);
            return Results.Created($"/expedientes/{resultado.Id}", resultado);
        });

        return app;
    }
}