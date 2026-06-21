namespace SGE.Dominio.Expedientes;
using SGE.Dominio.Excepciones;

public record class Caratula
{
    public string Texto {get; init;}
    public Caratula(string texto)
    {
        if (String.IsNullOrEmpty(texto))
        {
            throw new DominioException("la caratula no puede estar vacia");
        }

        Texto = texto;
    }

    protected Caratula() { }
}