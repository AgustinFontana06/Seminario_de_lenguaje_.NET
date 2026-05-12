namespace SGE.Aplicacion.Expedientes.Modificar;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Autorizaciones;

public class ModificarCaratulExpedienteUseCase(IExpedienteRepository repositorio, IAutorizacionService autorizacion)
{
    public ModificarCaratulaExpedienteResponse Ejecutar(ModificarCaratulaExpedienteRequest request)
    {
        //verifico autorizacion
        if(!autorizacion.PoseeElPermiso(request.idUsuario, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionExcepcion("No tienes permiso para modificar la caratula");
        }

        //obtengo su id
        Expediente? exp = repositorio.ObtenerPorId(request.idUsuario);

        if(exp == null)
        {
            //crear nueva excepcion, algo como ExcepcionIdentidadNoEncontrada
            throw new Exception($"No se encontro el expediente con el id {request.idUsuario}");
        }

        //creo caratula, ya paso las validaciones
        var nuevaCaratula = new Caratula(request.texto);
        exp.ModificarCaratula(nuevaCaratula, request.idUsuario);

        repositorio.Modificar(exp);

        return new ModificarCaratulaExpedienteResponse(exp.Id);
    }
}