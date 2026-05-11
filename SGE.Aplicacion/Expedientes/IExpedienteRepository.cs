using SGE.Dominio;
namespace SGE.Aplicacion;

public interface IExpedienteRepository{
    void Agregar(Expediente expedienteNuevo);
    void ObtenerPorId();
    void Modificar();
    void Eliminar();
}