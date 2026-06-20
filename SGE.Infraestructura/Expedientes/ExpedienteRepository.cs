namespace SGE.Infraestructura.Expedientes;

using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Abstracciones;
using SGE.Infraestructura.Datos;
using SGE.Infraestructura;

public class ExpedienteRepository : Repository<Expediente>, IExpedienteRepository
{
    public ExpedienteRepository(GestionContext context) : base(context)
    {
    }
}
