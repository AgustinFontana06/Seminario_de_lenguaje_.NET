namespace SGE.Aplicacion;
using SGE.Dominio;

public class AgregarExpedienteUseCase(IExpedienteRepository repositorio)
{
    public AgregarExpedienteResponse Ejecutar(AgregarExpedienteRequest request)
    {
        var caratula = new Caratula(request.caratulaText);
        //no es necesario un value object del user Id

        var expediente = new Expediente(caratula, request.idUsuario);
        //TODO: conectar con repositorio
        return new AgregarExpedienteResponse(expediente.Id);
    }

}