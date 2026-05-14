using SGE.Aplicacion.Comun;
using SGE.Dominio.Tramites;
namespace SGE.Aplicacion.Tramites.ObtenerPorId;

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