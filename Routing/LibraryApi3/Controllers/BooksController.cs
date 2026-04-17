using LibraryApi3.Data;
using LibraryApi3.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi3.Controllers
{
    [ApiController]
    public class BooksController : ControllerBase
    {
        // PROBLEM: Hardcoded routes - if controller name changes,
        // routes break!
        [Route("Books/GetAllBooks")]
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(BookData.Books);
        }

        [Route("Books/GetAvailableBooks")]
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAvailableBooks()
        {
            var books = BookData.Books.Where(b => b.IsAvailable).ToList();
            return Ok(books);
        }

        [Route("Books/GetBookById/{id}")]
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooksById(int id)
        {
            var book = BookData.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
    }
}

