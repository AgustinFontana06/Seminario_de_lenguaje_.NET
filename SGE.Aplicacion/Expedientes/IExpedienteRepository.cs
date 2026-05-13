namespace SGE.Aplicacion.Expedientes;
using SGE.Dominio.Expedientes;

public interface IExpedienteRepository{
    void Agregar(Expediente expedienteNuevo);
    Expediente? ObtenerPorId(Guid expedienteId);
    void Modificar(Expediente expedienteModificado);
    void Eliminar(Guid expedienteId);
    IEnumerable<Expediente> ObtenerTodos();
    
}