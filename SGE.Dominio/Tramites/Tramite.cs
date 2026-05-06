namespace SGE;

public class Tramite
{
    public Guid Id {get; private set;}
    public Guid ExpedienteId {get; private set;}
    public EtiquetaTramite Etiqueta {get; private set;}
    public ContenidoTramite Contenido {get; private set;}
    public DateTime FechaCreacion {get; private set;}
    public DateTime FechaUltimaModificacion {get; private set;}
    public Guid UsuarioUltimoCambio {get; private set;}

    public Tramite(Guid idExpediente, EtiquetaTramite etiqueta, ContenidoTramite contenido)
    {
        if(idExpediente == Guid.Empty)
        {
            throw new DominioException("El id no puede estar vacio");
        }

        Id = Guid.NewGuid();
        ExpedienteId = idExpediente;
        Etiqueta = etiqueta;
        Contenido = contenido;
        FechaCreacion = DateTime.Now;
        FechaUltimaModificacion = FechaCreacion;
        UsuarioUltimoCambio = Id;
    }

}