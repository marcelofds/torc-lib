using TorcLib.Data.Context;
using TorcLib.Domain.Aggregates;
using TorcLib.Domain.Aggregates.Repositories;

namespace TorcLib.Data.Repositories;

public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(LibraryContext context) : base(context)
    {
    }
}