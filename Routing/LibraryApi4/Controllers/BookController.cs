using LibraryApi4.Data;
using LibraryApi4.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi4.Controllers
{
    [ApiController]
    public class BookController : ControllerBase
    {
        // PROBLEM: "book" is repeated in every single route
        // If we rename to "Library", 
        // we have to change ALL three routes
        
        [Route("book/all")]
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(BookData.Books);
        }

        [Route("book/{id}")]
        [HttpGet]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = BookData.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [Route("book/available")]
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAvailableBooks()
        {
            var books = BookData.Books.Where(b => b.IsAvailable).ToList();
            return Ok(books);
        }
    }
}

