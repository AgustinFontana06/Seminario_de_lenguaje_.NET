namespace SGE.Aplicacion.Abstracciones;
using SGE.Dominio.Expedientes;

public interface IExpedienteRepository{
    void Agregar(Expediente expedienteNuevo);
    Expediente? ObtenerPorId(Guid expedienteId);
    void Modificar(Expediente expediente);
    void Eliminar(Guid expedienteId);
    IEnumerable<Expediente> ObtenerTodos();
}