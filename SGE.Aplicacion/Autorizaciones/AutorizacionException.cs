namespace SGE.Aplicacion.Autorizaciones;
using SGE.Dominio.Comun;
public class AutorizacionException : DominioException{
    public AutorizacionException(){}

    public AutorizacionException(string? message) : base(message){}

    public AutorizacionException(string? message, Exception? innerException) : base(message, innerException){}
}