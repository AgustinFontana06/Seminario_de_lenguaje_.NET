namespace SGE.Aplicacion.Expedientes.Modificar;
using SGE.Dominio.Expedientes;

public record class CambiarEstadoRequest(Guid idExpediente, EstadoExpediente estado, Guid idUsuario)
{
    
}