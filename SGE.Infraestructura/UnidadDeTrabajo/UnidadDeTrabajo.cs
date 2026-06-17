

public interface IUnidadDeTrabajo : IUnidadDeTrabajo
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