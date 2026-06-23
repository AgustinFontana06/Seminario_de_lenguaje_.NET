namespace SGE.Aplicacion.Expedientes.Agregar;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Excepciones;
using SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Permisos;

public class AgregarExpedienteUseCase(IExpedienteRepository repositorio, IAutorizacionService autorizacion, IUnidadDeTrabajo udt)
{
    public AgregarExpedienteResponse Ejecutar(AgregarExpedienteRequest request)
    {

        if(!autorizacion.PoseeElPermiso(request.idUsuario, Permiso.ExpedienteAlta))
        {
            throw new AutorizacionException("No tienes permiso para dar de alta el expediente.");
        }

        var caratula = new Caratula(request.caratulaText);
        //no es necesario un value object del user Id

        var expediente = new Expediente(caratula, request.idUsuario);
        repositorio.Agregar(expediente);
        udt.GuardarCambios();
        return new AgregarExpedienteResponse(expediente.Id);
    }
}