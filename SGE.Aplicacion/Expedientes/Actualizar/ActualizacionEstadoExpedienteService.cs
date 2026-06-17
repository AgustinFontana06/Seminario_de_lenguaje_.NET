namespace SGE.Aplicacion.Expedientes;

using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;
using System.Linq;

public class ActualizacionEstadoExpedienteService(IExpedienteRepository repositorioExpediente, ITramiteRepository repositorioTramite) : IActualizacionEstadoExpedienteService
{
    public void ActualizacionEstado(Guid ExpedienteId, Guid idUsuario)
    {
        Expediente? exp = repositorioExpediente.ObtenerPorId(ExpedienteId); 

        if(exp == null)
        {
            throw new EntidadNoEncontradaException($"No se encontro el expediente con el id {ExpedienteId}");
        }

        IEnumerable<Tramite> tramites = repositorioTramite.ObtenerPorExpedienteId(ExpedienteId);
        Tramite? UltimoTramite = tramites.MaxBy(t => t.FechaCreacion);

        bool cambio = exp.ActualizarEstado(UltimoTramite?.Etiqueta, idUsuario);

        if (cambio)
        {
            repositorioExpediente.Modificar(exp);
        }
    }
}