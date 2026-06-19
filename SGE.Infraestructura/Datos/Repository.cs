using SGE.Aplicacion.Abstracciones;
using EscuelaApi.Infraestructura;
using SGE.Dominio.Abstracciones;
using Microsoft.EntityFrameworkCore;

namespace SGE.Infraestructura.Datos;

public abstract class Repository<T> : IRepository<T> where T : Entidad
{
    protected readonly GestionContext _context;
    protected readonly DbSet<T> _dbSet;

    protected Repository(GestionContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual void Agregar(T entidad)
    {
        _dbSet.Add(entidad);
    }

    public virtual void Modificar(T entidad)
    {
        _dbSet.Update(entidad);
    }

    public virtual void Eliminar(Guid id)
    {
        var entidad = ObtenerPorId(id);
        if (entidad != null)
        {
            _dbSet.Remove(entidad);
        }
    }

    public virtual T? ObtenerPorId(Guid id)
    {
        return _dbSet.Find(id);
    }

    public virtual IEnumerable<T> ObtenerTodos()
    {
        return _dbSet.ToList();
    }
}
