namespace SGE.Aplicacion.Tramites.Modificar;
public record class ModificarTramiteRequest(Guid idTramite, string texto, Guid idUsuario){}