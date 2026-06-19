using SGE.Dominio.Abstracciones;

namespace SGE.Aplicacion.Abstracciones;

public interface IRepository<T> where T : Entidad
{
    void Agregar(T entidad);
    void Modificar(T entidad);
    void Eliminar(Guid id);
    T? ObtenerPorId(Guid id);
    IEnumerable<T> ObtenerTodos();
}