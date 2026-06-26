namespace SGE.Dominio.Tramites;
using SGE.Dominio.Excepciones;

public record class ContenidoTramite
{
    public string Texto{get; init;}

    public ContenidoTramite(string texto)
    {
        if (String.IsNullOrEmpty(texto))
        {
            throw new DominioException("el contenido del tramite no puede estar vacio");
        }

        Texto = texto;
    }

    protected ContenidoTramite(){}
}