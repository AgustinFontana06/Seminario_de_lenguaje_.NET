namespace SGE.Aplicacion.Expedientes.ObtenerTodos;
using SGE.Aplicacion.Expedientes.ObtenerPorId;

public record class ObtenerTodosExpedientesResponse(IEnumerable<ObtenerPorIdResponse> Expedientes);