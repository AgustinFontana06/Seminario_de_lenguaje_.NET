using SGE.Aplicacion.Abstracciones;
using SGE.Infraestructura.Datos;
using Microsoft.EntityFrameworkCore;


namespace SGE.Infraestructura.UDT;

public class UnidadDeTrabajo : IUnidadDeTrabajo
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