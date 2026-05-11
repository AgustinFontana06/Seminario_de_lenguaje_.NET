using System.Runtime.CompilerServices;
namespace SGE.Dominio;

public class DominioException : Exception
{
    //hola
    public DominioException(){}

    public DominioException(string? message) : base(message){}

    public DominioException(string? message, Exception? innerException) : base(message, innerException){}
}
