namespace SGE.Aplicacion.Comun;

public class EntidadNoEncontradaException : Exception
{
    //nueva excepcion creada
    public EntidadNoEncontradaException(){}

    public EntidadNoEncontradaException(string? message) : base(message){}

    public EntidadNoEncontradaException(string? message, Exception? innerException) : base(message, innerException){}
}