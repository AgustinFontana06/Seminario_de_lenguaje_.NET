namespace SGE.Aplicacion.Expedientes.Eliminar;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Abstracciones;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Tramites;
using SGE.Dominio.Permisos;


public class EliminarExpedienteUseCase(IExpedienteRepository repositorioExpediente, ITramiteRepository repositorioTramite,
    IAutorizacionService autorizacionService, IUnidadDeTrabajo udt)
{
    public EliminarExpedienteResponse Ejecutar(EliminarExpedienteRequest request)
    {
        if (!autorizacionService.PoseeElPermiso(request.idUsuario, Permiso.ExpedienteBaja)){
            throw new AutorizacionException("No tenes permiso para eliminar expedientes");
        }

        Expediente? exp = repositorioExpediente.ObtenerPorId(request.idExpediente);
        if (exp == null)
            throw new EntidadNoEncontradaException($"No se encontró un expediente con id {request.idExpediente}");
    
        IEnumerable<Tramite> tramites = repositorioTramite.ObtenerPorExpedienteId(request.idExpediente);
        foreach (var tramite in tramites)
        {
            repositorioTramite.Eliminar(tramite.Id);
        }

        // 4. Eliminar el expediente
        repositorioExpediente.Eliminar(request.idExpediente);


        udt.GuardarCambios();
        return new EliminarExpedienteResponse(exp.Id);

        //nota: se debe actualizar el ultimo cambio por usuario?
    }

}