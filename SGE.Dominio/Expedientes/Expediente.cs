namespace SGE;

public class Expediente
{
    public Guid Id {get; private set;}
    public Caratula Caratula {get; private set;}
    public DateTime FechaCreacion {get; private set;}
    public DateTime FechaUltimaModificacion {get; private set;}
    public Guid UsuarioUltimoCambio {get; private set;}
    public EstadoExpediente Estado {get; private set;}

    public Expediente(Caratula caratula, Guid idUsuario)
    {   
        if(idUsuario == Guid.Empty)
        {
            throw new DominioException("El id no puede estar vacio");
        }



        Id = Guid.NewGuid();
        UsuarioUltimoCambio = idUsuario;
        Caratula = caratula;
        FechaCreacion = DateTime.Now;
        FechaUltimaModificacion = FechaCreacion;
        Estado = EstadoExpediente.RecienIniciado;
    }

    public override string ToString()
    {
        return $"Expediente: {Id}, {Caratula.Texto}, {FechaCreacion}";
    }
}