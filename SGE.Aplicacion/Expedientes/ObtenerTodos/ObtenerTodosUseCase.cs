namespace SGE.Aplicacion.Expedientes.ObtenerTodos;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Expedientes.ObtenerPorId;

public class ObtenerTodosExpedientesUseCase(IExpedienteRepository repositorio)
{
    public ObtenerTodosExpedientesResponse Ejecutar()
    {
        IEnumerable<Expediente> expedientes = repositorio.ObtenerTodos();

        var respuesta = expedientes.Select(e => new ObtenerPorIdResponse(
            e.Id,
            e.Caratula.Texto,
            e.Estado,
            e.FechaCreacion,
            e.FechaUltimaModificacion,
            e.UsuarioUltimoCambio
        ));

        return new ObtenerTodosExpedientesResponse(respuesta);
    }
}