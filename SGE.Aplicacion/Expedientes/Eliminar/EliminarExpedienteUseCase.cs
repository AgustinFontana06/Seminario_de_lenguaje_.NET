namespace SGE.Aplicacion.Expediente.Eliminar;
using SGE.Dominio;

public class EliminarExpedienteUseCase(IExpedienteRepository repositorio)
{
    public EliminarExpedienteResponse Ejecutar(EliminarExpedienteRequest request)
    {
        //buscar por id
        //
        //var Id = repositorio.ObtenerPorId(request.Id);
        //
        //if(Id == null){
            //lanzar excepcion
        //}
        //repositorio.Eliminar(id)
    }

}