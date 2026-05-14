namespace SGE.Aplicacion.Expedientes.Agregar;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Autorizaciones;

public class AgregarExpedienteUseCase(IExpedienteRepository repositorio, IAutorizacionService autorizacion)
{
    public AgregarExpedienteResponse Ejecutar(AgregarExpedienteRequest request)
    {

        if(!autorizacion.PoseeElPermiso(request.idUsuario, Permiso.ExpendienteAlta))
        {
            throw new AutorizacionException("No tienes permiso para dar de alta el expediente.");
        }

        var caratula = new Caratula(request.caratulaText);
        //no es necesario un value object del user Id

        var expediente = new Expediente(caratula, request.idUsuario);
        //TODO: conectar con repositorio
        repositorio.Agregar(expediente);
        return new AgregarExpedienteResponse(expediente.Id);
    }
}