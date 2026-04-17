using LibraryApi3.Data;
using LibraryApi3.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi3.Controllers
{
    [ApiController]

    // define pattern ONCE at controller level - applices to ALL actions
    [Route("[controller]/[action]")]
    public class StaffController : ControllerBase
    {
        // No [ROute] attributes nedded here - inherits from controller
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBooks()
        {
            return Ok(BookData.Books);
        }

        // Inherits same automatically
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAvailableBooks()
        {
            var books = BookData.Books.Where(b => b.IsAvailable).ToList();
            return Ok(books);
        }

        // Can add parameters - pattern becomes /staff/GetBookById/3
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
    }
}

