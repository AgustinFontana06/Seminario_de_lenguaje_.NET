namespace SGE.Aplicacion.Expedientes.Eliminar;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Autorizaciones;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Comun;


public class EliminarExpedienteUseCase(IExpedienteRepository repositorioExpediente, ITramiteRepository repositorioTramite,
    IAutorizacionService autorizacionService)
{
    public EliminarExpedienteResponse Ejecutar(EliminarExpedienteRequest request)
    {
        if (!autorizacionService.PoseeElPermiso(request.idUsuario, Permiso.ExpedienteBaja)){
            throw new AutorizacionException("No tenes permiso para eliminar expedientes");
        }

        Expediente? exp = repositorioExpediente.ObtenerPorId(request.idExpediente);
        if (exp is null)
            throw new EntidadNoEncontradaException($"No se encontró un expediente con id {request.idExpediente}");
    
        IEnumerable<Tramite> tramites = repositorioTramite.ObtenerPorExpedienteId(request.idExpediente);
        foreach (var tramite in tramites)
        {
            repositorioTramite.Eliminar(tramite.Id);
        }

        // 4. Eliminar el expediente
        repositorioExpediente.Eliminar(request.idExpediente);

        return new EliminarExpedienteResponse(exp.Id);
    }

}