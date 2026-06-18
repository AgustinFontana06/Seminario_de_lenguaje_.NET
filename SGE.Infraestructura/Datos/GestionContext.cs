using SGE.Dominio;
using Microsoft.EntityFrameworkCore;

namespace EscuelaApi.Infraestructura;

public class GestionContext : DbContext
{
   
    public GestionContext(DbContextOptions<GestionContext> options) : base(options)
    {
    }

    public DbSet<Expediente> Expedientes { get; set; }
    public DbSet<Tramite> Tramites { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Le indicamos a EF Core que Email es un "Tipo Complejo" (Value Object)
        modelBuilder.Entity<Expediente>().complexProperty(e => e.Caratula);
        modelBuilder.Entity<Tramite>().complexProperty(t => t.ContenidoTramite);
        modelBuilder.Entity<Usuario>().ComplexProperty(u => u.Email);
    }
}