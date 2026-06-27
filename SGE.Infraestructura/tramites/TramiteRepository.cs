namespace SGE.Infraestructura.Tramites;

using SGE.Dominio.Tramites;
using SGE.Aplicacion.Abstracciones;
using SGE.Infraestructura.Datos;
public class TramiteRepository(SgeContext context) : Repository<Tramite>(context), ITramiteRepository
{
   
    public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId)
    {
        return _dbSet.Where(t => t.ExpedienteId == expedienteId).ToList();
    }
}

/*
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6Ijk0NTQ5ZTI4LTZmNDAtNDQ1Ni05M2ExLWEyNjAwODJkNmYzMyIsImV4cCI6MTc4MjUzNjI0NSwiaXNzIjoiV2ViQXBpIiwiYXVkIjoiQ0xpZW50ZXMifQ.eQ4ZoQTpm7MY6kXE-AKZLSBk8N_Tuw9-ruIyYw1RZUs
*/