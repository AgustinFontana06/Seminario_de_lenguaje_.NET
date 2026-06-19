namespace SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Tramites;

public interface ITramiteRepository : IRepository<Tramite>
{
    IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId);
}
