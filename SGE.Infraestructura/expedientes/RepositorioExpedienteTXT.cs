namespace SGE.Infraestructura.expedientes;
using SGE.Dominio;
using SGE.Aplicacion;

class RepositorioExpedienteTXT : IExpedienteRepository
{
    private readonly string _nombreArchivo = "expedientes.txt";

    public void Agregar(Expediente expedienteNuevo)
    {
        using var sw = new StreamWriter(_nombreArchivo, true);

        sw.WriteLine($"id del expediente: {expedienteNuevo.Id} ");
        sw.WriteLine($"Caratula: {expedienteNuevo.Caratula.Texto}");
        sw.WriteLine($"Fecha de creacion: {expedienteNuevo.FechaCreacion}");
        sw.WriteLine($"Estado: {expedienteNuevo.Estado}");
        sw.WriteLine();

        //no agregamos fecha de ultima modificacion ni usuario de ultimo cambio por redundancia
    }

}
