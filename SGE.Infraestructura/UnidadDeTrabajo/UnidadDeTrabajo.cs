using SGE.Aplicacion.Abstracciones;
using SGE.Infraestructura.
using SGE.Ap

namespace SGE.Infraestructura.UnidadDeTrabajo;

public interface UnidadDeTrabajo : IUnidadDeTrabajo
{
    private readonly GestionContext _context;
    public UnidadDeTrabajo(GestionContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void GuardarCambios()
    {
        _context.SaveChanges();
    }
}