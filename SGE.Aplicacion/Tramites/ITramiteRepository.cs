namespace SGE.Aplicacion.Tramites;
using SGE.Dominio.Tramites;
public interface ITramiteRepository
{
    void Agregar(Tramite tramite);
    void Modificar(Tramite tramite);
    void Eliminar(Guid tramiteId);
    Tramite? ObtenerPorId(Guid tramiteId);
    IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expedienteId);
}