namespace SGE.Aplicacion.Tramites.Dtos;
using SGE.Dominio.Tramites;

public record TramiteDto(Guid Id, EtiquetaTramite Etiqueta, ContenidoTramite Contenido, DateTime FechaCreacion, DateTime FechaUltimaModificacion, Guid UsuarioUltimoCambio){}