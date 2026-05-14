namespace SGE.Aplicacion.Tramites.ObtenerExpedientePorId;
using SGE.Dominio.Tramites;

public record ObtenerPorExpedienteIdResponse(IEnumerable<Tramite> tramites){}