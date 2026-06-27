using SGE.Dominio.Usuarios;
using Microsoft.EntityFrameworkCore;
using SGE.Dominio.Expedientes;
using System.Security.Cryptography;
using System.Text;
using SGE.Dominio.Permisos;

namespace SGE.Infraestructura.Datos;

public class SgeSqlite()
{
    public static void Inicializar(SgeContext context)
    {
        if (context.Database.EnsureCreated())
        {
            Console.WriteLine("Se creo la base de datos");

            // Establecemos la propiedad journal_mode de la base de datos SQLite en DELETE 
            var connection = context.Database.GetDbConnection();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA journal_mode=DELETE;";
                command.ExecuteNonQuery();
            }

            var hashAdmin = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes("admin123")));
            var admin = new Usuario("Admin del sistema", new DireccionEmail("admin", "sge.com"), hashAdmin);
            admin.setAdmin();
            context.Usuarios.Add(admin);

            var hashUsuario1 = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes("juan123")));
            var usuario1 = new Usuario("juan", new DireccionEmail("juan", "sge.com"), hashUsuario1);
            usuario1.AgregarPermiso(Permiso.ExpedienteAlta);
            usuario1.AgregarPermiso(Permiso.ExpedienteBaja);
            usuario1.AgregarPermiso(Permiso.TramiteAlta);
            context.Usuarios.Add(usuario1);

            var hashUsuario2 = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes("maria123")));
            var usuario2 = new Usuario("maria", new DireccionEmail("maria", "sge.com"), hashUsuario2);
            context.Usuarios.Add(usuario2);

            context.SaveChanges();    
        }
        

    }
}


