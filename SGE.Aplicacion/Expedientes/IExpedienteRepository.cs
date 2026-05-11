using SGE.Dominio;
namespace SGE.Aplicacion;

public interface IExpedienteRepository{
    void Agregar();
    void ObtenerPorId();
    void Modificar();
    void Eliminar();
}