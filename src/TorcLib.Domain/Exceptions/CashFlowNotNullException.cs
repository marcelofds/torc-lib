using System.Diagnostics.CodeAnalysis;
using TorcLib.Domain.Aggregates;

namespace TorcLib.Domain.Exceptions;

public class LibraryNotFoundException : Exception
{
    private const string ItemNotFoundMessage = "Item not found.";

    public LibraryNotFoundException(string message) : base(message)
    {
    }

    public static void ThrowWithTitleNotFoundMessageIfNull([NotNull] Book? title)
    {
        if (title == null) throw new LibraryNotFoundException(ItemNotFoundMessage);
    }
}