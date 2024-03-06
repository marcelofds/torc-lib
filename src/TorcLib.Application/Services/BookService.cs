using Mapster;
using TorcLib.Application.DataTransferObjects;
using TorcLib.Domain.Aggregates;
using TorcLib.Domain.Aggregates.Repositories;
using TorcLib.Domain.Exceptions;

namespace TorcLib.Application.Services;

public interface IBookService
{
    Task<BookDto?> GetBookByIdAsync(int id);
    Task IncludeNewBookAsync(BookDto book);
    Task DeleteBookAsync(int id);
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
}

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository toPayRepository)
    {
        _bookRepository = toPayRepository;
    }

    public async Task<BookDto?> GetBookByIdAsync(int id)
    {
        return (await _bookRepository.GetByIdAsync(id))
            .Adapt<BookDto>();
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        return
            new BookDto[]
            {
                new()
                {
                    Category = "Romantic",
                    Title = "Test1",
                    Id = 1,
                    Type = "Book",
                    FirstName = "Name1",
                    LastName = "Last1",
                    TotalCopies = 2,
                    CopiesInUse = 2,
                    ISBN = "123"
                },
                new () {Category = "Romantic",
                    Title = "Test2",
                    Id = 1,
                    Type = "Book",
                    FirstName = "Name2",
                    LastName = "Last2",
                    TotalCopies = 3,
                    CopiesInUse = 3,
                    ISBN = "1234"},
                new ()
                {
                    Category = "Drama",
                    Title = "Test3",
                    Id = 1,
                    Type = "Book",
                    FirstName = "Name3",
                    LastName = "Last3",
                    TotalCopies = 4,
                    CopiesInUse = 2,
                    ISBN = "12345"
                }
            };
        //(await _bookRepository.GetAllAsync()).Adapt<IEnumerable<BookDto>>();
    }
    

    public async Task IncludeNewBookAsync(BookDto book)
    {
        var entity = book.Adapt<Book>();
        _bookRepository.Insert(entity);
        await _bookRepository.SaveAsync();
    }

    public async Task DeleteBookAsync(int id)
    {
        var billToPay = await _bookRepository.GetByIdAsync(id)
                        ?? throw new LibraryNotFoundException("Record not found.");
        _bookRepository.Delete(billToPay);
        await _bookRepository.SaveAsync();
    }
    
}