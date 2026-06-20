namespace SGE.Aplicacion.Expedientes.Modificar;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Permisos;

public class ModificarCaratulaExpedienteUseCase(IExpedienteRepository repositorio, IAutorizacionService autorizacion, IUnidadDeTrabajo udt)
{
    public ModificarCaratulaExpedienteResponse Ejecutar(ModificarCaratulaExpedienteRequest request)
    {
        //verifico autorizacion
        if(!autorizacion.PoseeElPermiso(request.idUsuario, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("No tienes permiso para modificar la caratula");
        }

        //obtengo su id
        Expediente? exp = repositorio.ObtenerPorId(request.idExpediente);

        if(exp == null)
        {
            //creamos nueva exception(personalizada).
            throw new EntidadNoEncontradaException($"No se encontro el expediente con el id {request.idExpediente}");
        }

        //creo caratula, ya paso las validaciones
        var nuevaCaratula = new Caratula(request.texto);
        exp.ModificarCaratula(nuevaCaratula, request.idUsuario);

        repositorio.Modificar(exp);
        udt.GuardarCambios();

        return new ModificarCaratulaExpedienteResponse(exp.Id, exp.Caratula.Texto);
    }
}