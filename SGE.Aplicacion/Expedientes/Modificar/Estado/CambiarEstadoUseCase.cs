namespace SGE.Aplicacion.Expedientes.Modificar;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Permisos;


public class CambiarEstadoUseCase(IExpedienteRepository repositorio, IAutorizacionService autorizacion, IUnidadDeTrabajo udt)
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

        //nota: creo que no es necesario un propio metodo de repositorio de cambiar estado, con el modificar ya basta
        repositorio.Modificar(exp);
        udt.GuardarCambios();


        return new CambiarEstadoResponse(exp.Id, exp.Estado);
    }
}