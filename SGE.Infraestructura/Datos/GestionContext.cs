using SGE.Dominio.Tramites;
using SGE.Dominio.Usuario;
using SGE.Dominio.Expedientes;
using Microsoft.EntityFrameworkCore;

namespace SGE.Infraestructura.Datos;

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
        modelBuilder.Entity<Expediente>().ComplexProperty(e => e.Caratula);
        modelBuilder.Entity<Tramite>().ComplexProperty(t => t.Contenido);
        modelBuilder.Entity<Usuario>().ComplexProperty(u => u.Email);
    }
}