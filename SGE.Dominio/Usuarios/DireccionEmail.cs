namespace SGE.Dominio.Usuarios;
using SGE.Dominio.Excepciones;
public record class DireccionEmail
{
    public string Cuenta {get; private set;}="";
    public string Dominio {get; private set;}="";

    public DireccionEmail(string cuenta, string dominio){
        if(string.IsNullOrWhiteSpace(cuenta) || string.IsNullOrWhiteSpace(dominio))
        {
            throw new DominioException("La direccion email no puede estar vacia");
        }
        Cuenta = cuenta;
        Dominio = dominio;
    }

    //constructor vacio para EF CORE
    protected DireccionEmail(){}

    public static  DireccionEmail Parse(string emailCompleto){
        if (string.IsNullOrWhiteSpace(emailCompleto) || !emailCompleto.Contains('@'))
        {
            throw new DominioException("El formato del email es inválido.");
        }
        var partes = emailCompleto.Split('@');
        if (string.IsNullOrWhiteSpace(partes[0]) || string.IsNullOrWhiteSpace(partes[1]))
        {
            throw new DominioException("El formato del email es inválido.");
        }
        return new DireccionEmail(partes[0], partes[1]);
    }
    public override string ToString()
    {
        return $"{Cuenta}@{Dominio}";
    }
}