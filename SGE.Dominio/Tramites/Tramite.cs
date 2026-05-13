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

    public Tramite(Guid idExpediente, ContenidoTramite contenido, Guid usuarioUltimoCambio)
    {
        if(idExpediente == Guid.Empty)
        {
            throw new DominioException("El id no puede estar vacio");
        }

        Id = Guid.NewGuid();
        ExpedienteId = idExpediente;
        Etiqueta = EtiquetaTramite.EscritoPresentado; // TODO: escritoPresentado es el estado inicial?
        Contenido = contenido;
        FechaCreacion = DateTime.Now;
        FechaUltimaModificacion = FechaCreacion;
        UsuarioUltimoCambio = usuarioUltimoCambio;
    }
    public static Tramite Reconstruir (Guid id, Guid expedienteId,EtiquetaTramite etiqueta,ContenidoTramite contenido , DateTime fechaDeCreacion, DateTime fechaDeUltimaModificacion, Guid usuarioUltimoCambio)
    {
        // no debe cheque nada debido  que solo reconstruye el expediente que antes ya creado

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