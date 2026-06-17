namespace SGE.Dominio.Excepciones;
using System.Runtime.CompilerServices;

public class DominioException : Exception
{

    public DominioException(){}

    public DominioException(string? message) : base(message){}

    public DominioException(string? message, Exception? innerException) : base(message, innerException){}
}
