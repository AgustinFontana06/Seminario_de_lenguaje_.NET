namespace SGE.Aplicacion.Tramites.ObtenerExpedientePorId;
using SGE.Dominio.Tramites;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Tramites.Dtos;

public record ObtenerPorExpedienteIdResponse(Guid IdExpediente,
    string Caratula,
    EstadoExpediente Estado,
    DateTime FechaCreacion,
    DateTime FechaUltimaModificacion,
    IEnumerable<TramiteDto> Tramites){}
