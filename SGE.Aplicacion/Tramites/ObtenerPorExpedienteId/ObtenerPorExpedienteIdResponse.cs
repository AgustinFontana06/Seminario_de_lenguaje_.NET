namespace SGE.Aplicacion.Tramites.ObtenerExpedientePorId;
using SGE.Dominio.Tramites;
using SGE.Aplicacion.Tramites.Dtos;

public record ObtenerPorExpedienteIdResponse(IEnumerable<TramiteDto> Tramites){}
