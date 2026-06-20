namespace SGE.Infraestructura.Tramites;

using SGE.Dominio.Tramites;
using SGE.Aplicacion.Abstracciones;
using SGE.Infraestructura.Datos;
public class TramiteRepository(GestionContext context) : Repository<Tramite>(context), ITramiteRepository
{
   
    public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId)
    {
        return _dbSet.Where(t => t.ExpedienteId == expedienteId).ToList();
    }
}
