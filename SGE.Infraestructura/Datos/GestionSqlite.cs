using SGE.Infraestructura.Datos;
using SGE.Dominio;
using SGE.Dominio.Usuario;
using Microsoft.EntityFrameworkCore;
using SGE.Dominio.Expedientes;

namespace SGE.Infraestructura.Datos;

public class GestionSqlite()
{
    public static void Inicializar(GestionContext context)
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

            var usuario1 = new Usuario("Juan", new DireccionEmail("juan", ".com"), "12345");

            //agregamos usuario
            context.Usuarios.Add(usuario1);

            //agregamos expedientes y tramites
            context.Expedientes.Add(new Expediente(new Caratula("hola"), usuario1.Id));

            //agregamos administrador
            var admin = new Usuario("Admin del Sistema", new DireccionEmail("admin", ".com"),"admin123");
            context.Usuarios.Add(admin);

            context.SaveChanges();    
        }
        

    }
}


