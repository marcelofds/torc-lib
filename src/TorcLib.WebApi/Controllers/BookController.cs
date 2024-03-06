using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TorcLib.Application.DataTransferObjects;
using TorcLib.Application.Services;

namespace TorcLib.WebApi.Controllers;

/// <summary>
///     Endpoint to manage book in library
/// </summary>
[Produces("application/json")]
[Route("api/books")]
[Authorize]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;

    private readonly IBookService _service;

    public BookController(IBookService service, ILogger<BookController> logger)
    {
        _logger = logger;
        _service = service;
    }


    [HttpGet]
    public async Task<ActionResult<List<BookDto>>> GetAllAsync()
    {
        return (await _service.GetAllBooksAsync()).ToList();
    }

    /// <summary>
    ///     Finds out a bill to pay that fetches with {id}
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Null if it not found or the bill title fetched</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto?>> GetBillToPayByIdAsync(int id)
    {
        return await _service.GetBookByIdAsync(id);
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _service.DeleteBookAsync(id);
        _logger.LogInformation("The user ${User} has been deleted the bill to pay title ${Id}",
            HttpContext?.User?.Identity?.Name, id);
        return NoContent();
    }

    /// <summary>
    ///     Insert new bill to pay.
    /// </summary>
    /// <param name="bill"> bill title to insert</param>
    /// <returns>No content.</returns>
    [HttpPost]
    public async Task<IActionResult> AddNewAsync([FromBody] BookDto book)
    {
        await _service.IncludeNewBookAsync(book);
        _logger.LogInformation("The user ${User} has been added the book  title ${@Book}",
            HttpContext?.User?.Identity?.Name, book);
        return NoContent();
    }
}