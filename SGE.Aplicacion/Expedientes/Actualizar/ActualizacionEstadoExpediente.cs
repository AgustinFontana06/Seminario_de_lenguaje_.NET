namespace SGE.Aplicacion.Expedientes;

using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;

public class ActualizacionEstadoExpediente(IExpedienteRepository repositorioExpediente, ITramiteRepository repositorioTramite)
{
    public void Ejecutar(Guid ExpedienteId, Guid idUsuario)
    {
        Expediente? exp = repositorioExpediente.ObtenerPorId(ExpedienteId);

        if(exp == null)
        {
            throw new EntidadNoEncontradaException($"No se encontro el expediente con el id {ExpedienteId}");
        }

        IEnumerable<Tramite> tramites = repositorioTramite.ObtenerPorExpedienteId(ExpedienteId);
        Tramite? ultimoTramite = tramites.MaxBy(t => t.FechaCreacion);

        //si mi ultimo tramite es null (no hay) cambia el estado del expediente a Recien Iniciado
        bool cambio = exp.ActualizarEstado(ultimoTramite?.Etiqueta, idUsuario);

        if (cambio)
        {
            repositorioExpediente.Modificar(exp);
        }
    }
}