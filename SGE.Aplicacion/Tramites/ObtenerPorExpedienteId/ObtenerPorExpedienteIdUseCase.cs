namespace SGE.Aplicacion.Tramites.ObtenerExpedientePorId;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Tramites;
using SGE.Dominio.Expedientes;
using System.Linq;
using SGE.Aplicacion.Tramites.Dtos;

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

        return new ObtenerPorExpedienteIdResponse(exp.Id, exp.Caratula.Texto, exp.Estado, exp.FechaCreacion, exp.FechaUltimaModificacion, tramitesDto);
    }
}