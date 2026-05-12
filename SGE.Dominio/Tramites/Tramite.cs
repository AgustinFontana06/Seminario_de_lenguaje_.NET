namespace SGE.Dominio.Tramites;
using SGE.Dominio.Comun;

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
    public static Tramite Reconstruir (Guid id, Guid expedienteId,EtiquetaTramite etiqueta,ContenidoTramite contenido , DateTime fechaDeCreacion, DateTime fechaDeUltimaModificacion, Guid usuarioUltimoCambio)
    {
        // ESTO REVISAR ESTA RARO;
        if (fechaDeUltimaModificacion < fechaDeCreacion)
        {
            throw new DominioException ("la fecha de modificacion no puede ser menor a la fecha de creacion");
        }
        Tramite nuevoTramite  = new Tramite (id,expedienteId,etiqueta,contenido,fechaDeCreacion,fechaDeUltimaModificacion, usuarioUltimoCambio);
        return nuevoTramite;
    }

    private Tramite(Guid id, Guid expedienteId, EtiquetaTramite etiqueta, ContenidoTramite contenido, DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid usuarioUltimoCambio)
    {
        Id = id;
        ExpedienteId = expedienteId;
        Etiqueta = etiqueta;
        Contenido = contenido;
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaUltimaModificacion;
        UsuarioUltimoCambio = usuarioUltimoCambio;
    }

}