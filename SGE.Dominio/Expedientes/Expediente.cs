namespace SGE.Dominio.Expedientes;
using System.Diagnostics.CodeAnalysis;
using SGE.Dominio.Comun;
using SGE.Dominio.Tramites;

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

    public static Expediente Reconstruir (Guid id, Caratula caratula, DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid usuarioUltimoCambio, EstadoExpediente estado)
    {
        // ESTO REVISAR ESTA RARO;
        if (fechaUltimaModificacion < fechaCreacion)
        {
            throw new DominioException("la fecha de modificacion no puede ser menor a la fecha de creacion");
        }


        return new Expediente(id, caratula, fechaCreacion, fechaUltimaModificacion, usuarioUltimoCambio, estado);
    }
    private Expediente(Guid id, Caratula caratula, DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid usuarioUltimoCambio, EstadoExpediente estado)
    {
        Id = id;
        Caratula = caratula;
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaUltimaModificacion;
        UsuarioUltimoCambio = usuarioUltimoCambio;
        Estado = estado;
    }

    public void RegistrarCambio(Guid idUsuario)
    {
        UsuarioUltimoCambio = idUsuario;
        FechaUltimaModificacion = DateTime.Now;
    }

    public void ModificarCaratula(Caratula nuevaCaratula, Guid idUsuario)
    {
        if(idUsuario == Guid.Empty)
        {
            throw new DominioException("El id no puede estar vacio");
        }

        Caratula = nuevaCaratula;
        RegistrarCambio(idUsuario);
    }

    public bool ActualizarEstado(EtiquetaTramite? ultimaEtiqueta, Guid idUsuario)
    {
        if(idUsuario == Guid.Empty)
        {
            throw new DominioException("El id no puede estar vacio");
        }

        EstadoExpediente estadoAnterior = Estado;
        EstadoExpediente nuevoEstado;

        if(ultimaEtiqueta == null)
        {
            nuevoEstado = EstadoExpediente.RecienIniciado;
        } else
        {
            switch (ultimaEtiqueta) {
                case EtiquetaTramite.Resolucion:
                    nuevoEstado = EstadoExpediente.ConResolucion;
                    break;
                case EtiquetaTramite.PaseAEstudio:
                    nuevoEstado = EstadoExpediente.ParaResolver;
                    break;
                case EtiquetaTramite.PaseAlArchivo:
                    nuevoEstado = EstadoExpediente.Finalizado;
                    break;
                default:
                    nuevoEstado = Estado;
                    break;
            }
        }

        if(nuevoEstado != estadoAnterior)
        {
            Estado = nuevoEstado;
            RegistrarCambio(idUsuario);
            return true;
        }
        return false;
    }

    public void CambiarEstado(EstadoExpediente nuevoEstado, Guid idUsuario)
    {
        if(idUsuario == Guid.Empty)
        {
            throw new DominioException("El id no puede estar vacio");
        }

        Estado = nuevoEstado;
        RegistrarCambio(idUsuario);
    }

    public override string ToString()
    {
        return $"Expediente: {Id}, {Caratula.Texto}, {FechaCreacion}";
    }
}