using LibraryApi2.Data;
using LibraryApi2.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        // THREE URLs point to this SINGLE method
        [HttpGet("All")]            // URL  1: /api/books/All
        [HttpGet("AllBooks")]       // URL  2: /api/books/AllBooks
        [HttpGet("GetAllBooks")]    // URL  3: /api/books/GetAllBooks
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            var books = BookData.Books;
            return Ok(books);
        }
    }
}

