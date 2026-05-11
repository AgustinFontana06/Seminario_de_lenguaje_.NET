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
        sw.WriteLine("motivo: ", expedienteNuevo.Caratula.Texto);
        sw.WriteLine("");
        sw.WriteLine();
        sw.WriteLine();
    }

}
