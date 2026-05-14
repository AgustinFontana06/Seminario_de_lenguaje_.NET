namespace SGE.Aplicacion.Expedientes.ObtenerTodos;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Expedientes.ObtenerPorId;

public class ObtenerTodosUseCase(IExpedienteRepository repositorio)
{
    public ObtenerTodosResponse Ejecutar()
    {
        IEnumerable<Expediente> expedientes = repositorio.ObtenerTodos();

        //por cada Expediente encontrado, crea un Dto de respuesta
        var respuesta = expedientes.Select(e => new ObtenerPorIdResponse(
            e.Id,
            e.Caratula.Texto,
            e.Estado,
            e.FechaCreacion,
            e.FechaUltimaModificacion,
            e.UsuarioUltimoCambio
        ));

        //envio toda la lista de dtos
        return new ObtenerTodosResponse(respuesta);
    }
}