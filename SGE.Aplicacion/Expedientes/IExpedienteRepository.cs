namespace SGE.Aplicacion.Expedientes;
using SGE.Dominio.Expedientes;

public interface IExpedienteRepository{
    void Agregar(Expediente expedienteNuevo);
    Expediente? ObtenerPorId(Guid expedienteId);
    void Modificar(string texto, Guid expedienteId, Guid usuarioId);
    void CambiarEstado(EstadoExpediente estadoNuevo ,Guid usuarioId);
    void Eliminar(Guid expedienteId);
    IEnumerable<Expediente> ObtenerTodos();
    
}