namespace SGE.Aplicacion.Autorizaciones;
using SGE.Dominio.Comun;
public class AutorizacionExcepcion : DominioException{
    public AutorizacionExcepcion(){}

    public AutorizacionExcepcion(string? message) : base(message){}

    public AutorizacionExcepcion(string? message, Exception? innerException) : base(message, innerException){}
}