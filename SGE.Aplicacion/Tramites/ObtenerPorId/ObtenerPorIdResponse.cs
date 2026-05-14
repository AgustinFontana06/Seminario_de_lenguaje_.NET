namespace SGE.Aplicacion.Tramites.ObtenerPorId;
using SGE.Dominio.Tramites;

public record ObtenerPorIdResponse(Guid id, Guid expedienteId, EtiquetaTramite etiqueta, ContenidoTramite contenido, DateTime fechaDeCreacion, DateTime fechaUltimoCambio, Guid usuarioUltimoCambio){}