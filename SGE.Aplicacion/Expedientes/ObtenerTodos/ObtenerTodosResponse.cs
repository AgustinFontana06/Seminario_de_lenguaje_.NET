namespace SGE.Aplicacion.Expedientes.ObtenerTodos;
using SGE.Aplicacion.Expedientes.ObtenerPorId;

public record class ObtenerTodosResponse(IEnumerable<ObtenerPorIdResponse> Expedientes);