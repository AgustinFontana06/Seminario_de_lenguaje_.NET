using SGE.Aplicacion.Abstracciones;
using SGE.Infraestructura.Datos;
using Microsoft.EntityFrameworkCore;


namespace SGE.Infraestructura.UDT;

public class UnidadDeTrabajo : IUnidadDeTrabajo
{
    private readonly SgeContext _context;
    public UnidadDeTrabajo(SgeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Guardar()
    {
        _context.SaveChanges();
    }
}