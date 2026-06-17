namespace SGE.Dominio.Expedientes;
using System.Diagnostics.CodeAnalysis;
using SGE.Dominio.Excepciones;
using SGE.Dominio.Tramites;
using SGE.Dominio.Abstracciones;

public class Expediente : Entidad
{
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

        UsuarioUltimoCambio = idUsuario;
        Caratula = caratula;
        FechaCreacion = DateTime.Now;
        FechaUltimaModificacion = FechaCreacion;
        Estado = EstadoExpediente.RecienIniciado;
    }

    public static Expediente Reconstruir (Guid id, Caratula caratula, DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid usuarioUltimoCambio, EstadoExpediente estado)
    {
        // no debe cheque nada debido  que solo reconstruye el expediente que antes ya creado
        return new Expediente(id, caratula, fechaCreacion, fechaUltimaModificacion, usuarioUltimoCambio, estado);
    }
    private Expediente(Guid id, Caratula caratula, DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid usuarioUltimoCambio, EstadoExpediente estado)
    {
        if(fechaUltimaModificacion < fechaCreacion)
        {
            throw new DominioException("La fecha de ultima modificacion no puede ser menor que la fecha de creacion");
        }
        Id = id;
        Caratula = caratula;
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaUltimaModificacion;
        UsuarioUltimoCambio = usuarioUltimoCambio;
        Estado = estado;
    }   
    

    private void RegistrarCambio(Guid idUsuario)
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
        Boolean cambio;
        if(nuevoEstado != estadoAnterior)
        {
            Estado = nuevoEstado;
            RegistrarCambio(idUsuario);
            cambio = true;
        } else
        {
            cambio = false;
        }
        return cambio;
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