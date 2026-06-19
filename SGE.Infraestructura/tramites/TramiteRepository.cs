namespace SGE.Infraestructura.Tramites;

using SGE.Dominio.Tramites;
using SGE.Aplicacion.Abstracciones;
using SGE.Infraestructura.Datos;
using EscuelaApi.Infraestructura;

public class TramiteRepository : Repository<Tramite>, ITramiteRepository
{
    public TramiteRepository(GestionContext context) : base(context)
    {
    }

    public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId)
    {
        return _dbSet.Where(t => t.ExpedienteId == expedienteId).ToList();
    }
}
