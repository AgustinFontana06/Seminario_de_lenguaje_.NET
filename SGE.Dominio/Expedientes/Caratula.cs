namespace SGE;

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
}