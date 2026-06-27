namespace SGE.Aplicacion.Expedientes.Modificar;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Permisos;


public class CambiarEstadoUseCase(IExpedienteRepository repositorio, IAutorizacionService autorizacion, IUnidadDeTrabajo udt)
{
    public CambiarEstadoResponse Ejecutar(CambiarEstadoRequest request, Guid idUsuario, Guid idExpediente)
    {
        //verifico autorizacion
        if(!autorizacion.PoseeElPermiso(idUsuario, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("No tienes permiso para modificar la caratula");
        }

        Expediente? exp = repositorio.ObtenerPorId(idExpediente);

        if(exp == null)
        {
            throw new EntidadNoEncontradaException($"No se encontro el expediente con el id {idExpediente}");
        }


        exp.CambiarEstado(request.estado, idUsuario);

        //nota: creo que no es necesario un propio metodo de repositorio de cambiar estado, con el modificar ya basta
        repositorio.Modificar(exp);
        udt.Guardar();


        return new CambiarEstadoResponse(exp.Id, exp.Estado);
    }
}