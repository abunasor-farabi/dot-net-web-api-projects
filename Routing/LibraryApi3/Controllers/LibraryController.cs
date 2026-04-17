using LibraryApi3.Data;
using LibraryApi3.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi3.Controllers
{
    [ApiController]

    // [controller] is replaced with "Library" (class
    // name without "Controller")
    [Route("[controller]")]
    public class LibraryController : ControllerBase
    {
        // ====================================================
        // METHOD 1: Token at Controller level only
        // URL becomes: /Library/GetAllBooks
        // ====================================================
        [Route("[action]")]
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(BookData.Books);
        }

        // =====================================================
        // METHOD 2: Combined token at Controller level
        // URL becomes: /Library/GetAvailableBooks
        // =====================================================
        [Route("[action]")]
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAvailableBooks()
        {
            var books = BookData.Books.Where(b => b.IsAvailable).ToList();
            return Ok(books);
        }

        // =====================================================
        // METHOD 3: Token + Route Parameter
        // URL becomes: /Library/GetBookById/3
        // =====================================================
        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = BookData.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound($"Book with ID {id} not found");
            }
            return Ok(book);
        }
    }
}

