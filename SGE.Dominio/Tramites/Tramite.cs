namespace SGE.Dominio.Tramites;
using SGE.Dominio.Excepciones;
using SGE.Dominio.Abstracciones;

public class Tramite : Entidad
{

    public Guid ExpedienteId {get; private set;}
    public EtiquetaTramite Etiqueta {get; private set;}
    public ContenidoTramite Contenido {get; private set;}
    public DateTime FechaCreacion {get; private set;}
    public DateTime FechaUltimaModificacion {get; private set;}
    public Guid UsuarioUltimoCambio {get; private set;}

    public Tramite(Guid idExpediente, ContenidoTramite contenido, Guid usuarioUltimoCambio)
    {
        if(idExpediente == Guid.Empty)
        {
            throw new DominioException("El id no puede estar vacio");
        }

        ExpedienteId = idExpediente;
        Etiqueta = EtiquetaTramite.EscritoPresentado;
        Contenido = contenido;
        FechaCreacion = DateTime.Now;
        FechaUltimaModificacion = FechaCreacion;
        UsuarioUltimoCambio = usuarioUltimoCambio;
    }

    protected Tramite() {}

    private Tramite(Guid id, Guid expedienteId, EtiquetaTramite etiqueta, ContenidoTramite contenido, DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid usuarioUltimoCambio)
    {
        if(fechaUltimaModificacion < fechaCreacion)
        {
            throw new DominioException("La fecha de ultima modificacion no puede ser menor que la fecha de creacion");
        }
        Id = id;
        ExpedienteId = expedienteId;
        Etiqueta = etiqueta;
        Contenido = contenido;
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaUltimaModificacion;
        UsuarioUltimoCambio = usuarioUltimoCambio;
    }

    public void ModificarContenido(ContenidoTramite nuevoContenido, Guid idUsuario)
    {
        if(idUsuario == Guid.Empty)
        {
            throw new DominioException("El id no puede estar vacio");
        }
        Contenido = nuevoContenido;
        RegistrarCambio(idUsuario);
    }

    
    public override string ToString()
    {
        return $"Tramite: {Id}, expediente asociado: {ExpedienteId}, fecha de creación:  {FechaCreacion}, contenido: {Contenido}";
    }

    private void RegistrarCambio(Guid idUsuario)
    {
        UsuarioUltimoCambio = idUsuario;
        FechaUltimaModificacion = DateTime.Now;
    }
}