namespace SGE.Aplicacion.Expedientes.Modificar;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Autorizaciones;
using SGE.Aplicacion.Comun;


public class CambiarEstadoUseCase(IExpedienteRepository repositorio, IAutorizacionService autorizacion)
{
    public CambiarEstadoResponse Ejecutar(CambiarEstadoRequest request)
    {
        //verifico autorizacion
        if(!autorizacion.PoseeElPermiso(request.idUsuario, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("No tienes permiso para modificar la caratula");
        }

        Expediente? exp = repositorio.ObtenerPorId(request.idExpediente);

        if(exp == null)
        {
            throw new EntidadNoEncontradaException($"No se encontro el expediente con el id {request.idUsuario}");
        }


        exp.CambiarEstado(request.estado, request.idUsuario);


        return new CambiarEstadoResponse(exp.Id, exp.Estado);
    }
}