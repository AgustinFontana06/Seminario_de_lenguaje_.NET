namespace SGE.Aplicacion.Tramites.ObtenerExpedientePorId;
using SGE.Dominio.Tramites;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Comun;
using System.Linq;

public class ObtenerPorExpedienteIdUseCase(IExpedienteRepository repositorioExpediente, ITramiteRepository repositorioTramite)
{
    public ObtenerPorExpedienteIdResponse Ejecutar(ObtenerPorExpedienteIdRequest request)
    {
        Expediente? exp = repositorioExpediente.ObtenerPorId(request.idExpediente);
        
        if(exp == null)
        {
            throw new EntidadNoEncontradaException($"no se encontro un expediente con el id {request.idExpediente}");
        }

        IEnumerable<Tramite> tramites = repositorioTramite.ObtenerPorExpedienteId(request.idExpediente);

        IEnumerable<TramiteDto> tramitesDto = tramites.Select(t => new TramiteDto(t.Id, t.Etiqueta, t.Contenido, t.FechaCreacion, t.FechaUltimaModificacion, t.UsuarioUltimoCambio));

        return new ObtenerPorExpedienteIdResponse(tramitesDto);
    }
}