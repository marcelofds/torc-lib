namespace TorcLib.Domain.Exceptions;

public class LibraryInvalidOperationException : Exception
{
    public LibraryInvalidOperationException(string message) : base(message)
    {
    }
}