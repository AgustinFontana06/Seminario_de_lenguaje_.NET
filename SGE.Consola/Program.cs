using SGE.Dominio.Comun;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;

//prueba de funcionamiento
var caratula = new Caratula("hola");

Guid id = Guid.NewGuid();

var expediente = new Expediente(caratula, id);

Console.WriteLine(expediente.ToString());

bool cambio = expediente.ActualizarEstado(EtiquetaTramite.Resolucion, id);

if (cambio)
{
    Console.WriteLine($"nuevo estado {expediente.Estado}");
    Console.WriteLine($"ultima modificacion {expediente.FechaUltimaModificacion}");
}

var nuevaCaratula = new Caratula("chau");
var caratulaError = new Caratula("");

try
{
    expediente.ModificarCaratula(nuevaCaratula, id);
    Console.WriteLine($"nueva caratula{nuevaCaratula}"); 
    expediente.ModificarCaratula(caratulaError, id);
}
catch (DominioException ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
catch (Exception ex) 
{
    Console.WriteLine($"Error inesperado: {ex.Message}");
}
