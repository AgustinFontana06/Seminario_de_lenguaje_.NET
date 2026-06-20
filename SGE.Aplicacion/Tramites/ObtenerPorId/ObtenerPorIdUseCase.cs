namespace SGE.Aplicacion.Tramites.ObtenerPorId;
using SGE.Aplicacion.Excepciones;
using SGE.Dominio.Tramites;
using SGE.Aplicacion.Abstracciones;

public class ObtenerPorIdUseCase(ITramiteRepository repositorioTramite)
{
    public ObtenerPorIdResponse Ejecutar(ObtenerPorIdRequest request)
    {
        Tramite? tramite = repositorioTramite.ObtenerPorId(request.idTramite);

        if(tramite == null)
        {
            throw new EntidadNoEncontradaException($"No se encontro un tramite con el id {request.idTramite}");
        }

        return new ObtenerPorIdResponse(
            tramite.Id,
            tramite.ExpedienteId,
            tramite.Etiqueta,
            tramite.Contenido,
            tramite.FechaCreacion,
            tramite.FechaUltimaModificacion,
            tramite.UsuarioUltimoCambio
        );
    }
}