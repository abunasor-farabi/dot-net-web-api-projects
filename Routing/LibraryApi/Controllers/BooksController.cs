using LibraryApi.Data;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    // ==========================================================
    // Section 1: ATTRIBUTE ROUTING (Preferred for webapi)
    // ==========================================================
    [ApiController]
    [Route("api/[controller]")]
    // Route: api/books
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =======================================================
        // GET: api/books
        // Return all books (using LINQ)
        // =======================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            // LINQ: Get all books from database
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }

        // =======================================================
        // GET: api/books/available
        // Returns only available books (Custom route with LINQ Where)
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAvailableBooks()
        {
            // LINQ: Filter books that are available
            var availableBooks = await _context.Books.Where(b => b.IsAvailable == true)
                                                    .ToListAsync();
            return Ok(availableBooks);
        }

        // ========================================================
        // GET: api/books/genre/{genre}
        // Returns books by genre (Route parameter with LINQ)
        // ========================================================
        [HttpGet("genre/{genre}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByGenre(string genre)
        {
            // LINQ: Filter by genre (case-insensitive)
            var books = await _context.Books.Where(b => b.Genre
                                            .ToLower() == genre
                                            .ToLower()).ToListAsync();
            if (!books.Any())
            {
                return NotFound($"No books found in genre: {genre}");
            }

            return Ok(books);
        }

        // =========================================================
        // GET: api/books/{id}
        // Returns single book by ID (Route parameter)
        // =========================================================
        [HttpGet("{id:int}")] // :int is a route constraint
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            // LINQ: Find specific book by ID
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if(book == null)
            {
                return NotFound($"Book with ID {id} not found");
            }

            return Ok(book);
        }

        // ==========================================================
        // GET: api/books/search?author=King&year=2020
        // Demonstrates QUERY STRING parameters
        // ==========================================================
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooks
        ([FromQuery] string? author, [FromQuery] int? year, [FromQuery] string? genre)
        {
            // LINQ: Start with all books
            var query = _context.Books.AsQueryable();

            // Apply filters only if parameters are provided
            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(b => b.Author.Contains(author));
            }

            if (year.HasValue)
            {
                query = query.Where(b => b.PublishedYear == year.Value);
            }

            if (!string.IsNullOrEmpty(genre))
            {
                query = query.Where(b => b.Genre.Contains(genre));
            }

            var results = await query.ToListAsync();

            if (!results.Any())
            {
                return NotFound("No books match your search criteria");
            }

            return Ok(results);
        }

        // ============================================================
        // POST: api/books
        // Creates a new book (demonstrates model building from body)
        // ============================================================
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook([FromBody] Book newBook)
        {
            // LINQ: Check if book with same title exists
            bool exists = await _context.Books.
            AnyAsync(b => b.Title.ToLower() == newBook.Title.ToLower());

            if (exists)
            {
                return BadRequest("A book with this title already exists");
            }

            // Add new book
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            // Returns 201 Created with Location header
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id },
             newBook);
        }

        // =============================================================
        // PUT: api/books/{id}
        // Updates an entire book
        // =============================================================
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
        {
            if (id != updatedBook.Id)
            {
                return BadRequest("ID mismatch");
            }

            // LINQ: Check if book exists
            var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (existingBook == null)
            {
                return NotFound($"Book with ID {id} not found");
            }

            // Update properties
            existingBook.Title = updatedBook.Title;
            existingBook.Author = updatedBook.Author;
            existingBook.Genre = updatedBook.Genre;
            existingBook.PublishedYear = updatedBook.PublishedYear;
            existingBook.IsAvailable = updatedBook.IsAvailable;
            existingBook.Price = updatedBook.Price;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Book updated successfully", book = existingBook});
        }

        // ===============================================================
        // DELETE: api/books/{id}
        // Delete a book
        // ===============================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            // LINQ: Find book
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound($"Book with ID {id} not found");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Book '{book.Title}' deleted successfully"});
        }

        // ================================================================
        // GET: api/books/starts
        // Shows various LINQ aggregate functions
        // ================================================================
        [HttpGet("starts")]
        public async Task<IActionResult> GetLibraryStarts()
        {
            var allBooks = await _context.Books.ToListAsync();

            // Demonstrate various LINQ methods
            var starts = new
            {
                TotalBooks = allBooks.Count,
                AvailableBooks = allBooks.Count(b => b.IsAvailable),
                CheckedOutBooks = allBooks.Count(b => !b.IsAvailable),
                AveragePrice = allBooks.Average(b => b.Price),
                MostExpensiveBook = allBooks.Max(b => b.Price),
                CheapestBook = allBooks.Min(b => b.Price),
                UniqueGenres = allBooks.Select(b => b.Genre).Distinct().Count(),
                BooksByYear = allBooks.GroupBy(b => b.PublishedYear).Select(g =>
                 new { Year = g.Key, Count = g.Count() }).OrderByDescending(g => g.Year)
            };

            return Ok(starts);
        }
    }
}

