using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;
using SGE.Dominio.Permisos;
using SGE.Dominio.Usuarios;

namespace SGE.Infraestructura.Datos;

public class GestionContext : DbContext
{
   
    public GestionContext(DbContextOptions<GestionContext> options) : base(options)
    {
    }

    public DbSet<Expediente> Expedientes { get; set; }
    public DbSet<Tramite> Tramites { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    //en esta clase se conecta la base de datos con el codigo, seria como un puente donde 
    //se transfieren datos, al heredarlo con DbContext le estamos diciendo que tiene la posibilidad
    //de manipular dicha base de datos, por eso se mapean las tablas Expedientes, Tramites y Usuarios

    //este metodo se ejecuta al principio del programa permitiendo adaptar la base de datos con
    //las clases creadas en el dominio
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Le indicamos a EF Core que Email es un "Tipo Complejo" (Value Object)
        modelBuilder.Entity<Expediente>().ComplexProperty(e => e.Caratula);
        modelBuilder.Entity<Tramite>().ComplexProperty(t => t.Contenido);
        modelBuilder.Entity<Usuario>().ComplexProperty(u => u.Email);

        //le asignamos las relaciones, un expediente tiene muchos tramites
        //si se borra un expediente se borran sus tramites
        modelBuilder.Entity<Tramite>().HasOne<Expediente>().WithMany().HasForeignKey(t => t.ExpedienteId).OnDelete(DeleteBehavior.Cascade);
    
        //convertir el enumerativo de permisos en json para sqlite y viceversa
        modelBuilder.Entity<Usuario>().Property(u => u.ListaDePermisos).HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions?) null),
            v => JsonSerializer.Deserialize<List<Permiso>>(v, (JsonSerializerOptions?)null) ?? new List<Permiso>()
        );
    }
}