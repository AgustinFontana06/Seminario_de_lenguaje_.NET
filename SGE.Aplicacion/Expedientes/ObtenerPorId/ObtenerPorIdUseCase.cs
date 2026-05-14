namespace SGE.Aplicacion.Expedientes.ObtenerPorId;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Comun;

public class ObtenerPorIdUseCase(IExpedienteRepository repositorio)
{
    public ObtenerPorIdResponse Ejecutar(ObtenerPorIdRequest request)
    {

        Expediente? expediente = repositorio.ObtenerPorId(request.ExpedienteId);

        if (expediente == null)
            throw new EntidadNoEncontradaException($"No se encontro un expediente con id {request.ExpedienteId}");


        return new ObtenerPorIdResponse(
            expediente.Id,
            expediente.Caratula.Texto,
            expediente.Estado,
            expediente.FechaCreacion,
            expediente.FechaUltimaModificacion,
            expediente.UsuarioUltimoCambio
        );
    }
}