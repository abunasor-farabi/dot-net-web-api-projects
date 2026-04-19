using LibraryApi5.Data;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        // ==================================================
        // TYPE CONSTRAINT  :int - Only numeric IDs  allowed
        // URL: GET /api/book/5
        // Invalid: /api/book/abc --> 404
        // ==================================================
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var book = BookData.Books.FirstOrDefault(b => b.Id == id);
            return book is not null
                            ? Ok(book)
                            : NotFound($"No book found with ID: {id}");
        }

        // ====================================================
        // RANGE CONSTRAINT  - Ratting must be between 1 aned 5
        // URL: GET /api/book/rating/5
        // Invalid: /api/book/rating/9 --> 404
        // ====================================================
        [HttpGet("rating/{rating:int:range(1, 5)}")]
        public IActionResult GetByRating(int rating)
        {
            var books = BookData.Books.Where(b => b.Rating == rating)
                        .ToList();
            return books.Any()
                    ? Ok(books)
                    : NotFound($"No books found with rating: {rating}");
        }

        // =====================================================
        // ALPHA CONSTRAINT - Only letters allowed in genre
        // URL: GET /api/book/genre/fantasy
        // Invalid: /api/book/genre/fantasy123 --> 404
        // =====================================================
        [HttpGet("genre/{genre:alpha}")]
        public IActionResult GetByGenre(string genre)
        {
            var books = BookData.Books.Where(b => b.Genre
                .Equals(genre, StringComparison.OrdinalIgnoreCase)
            ).ToList();
            return books.Any()
                    ? Ok(books)
                    : NotFound($"No books found in genre: {genre}");
        }

        // ======================================================
        // LENGTH CONSTRAINT - ISBN must be exactly 17 characters
        // URL: GET /api/book/isbn/980-0-547-92822-7
        // ======================================================
        [HttpGet("isbn/{isbn:length(17)}")]
        public IActionResult GetByIsbn(string isbn)
        {
            var book = BookData.Books.FirstOrDefault(b => b.ISBN == isbn);
            return book is not null
                    ? Ok(book)
                    : NotFound($"No book found with ISBN: {isbn}");
        }

        // =======================================================
        // MINLENGTH & MAXLENGTH - Title between 3 & 50 characters
        // URL: GET /api/book/title/The Hobbit
        // =======================================================
        [HttpGet("title/{title:minlength(3):maxlength(50)}")]
        public IActionResult GetByTitle(string title)
        {
            var book = BookData.Books.FirstOrDefault(b => b.Title.Equals(
                title, StringComparison.OrdinalIgnoreCase
            ));
            return book is not null
                    ? Ok(book)
                    : NotFound($"No book found with title: {title}");
        }

        // ========================================================
        // REGIX CONSTRAINT - Author name (letters, spaces, dots 
        // allowed)
        // URL: GET /api/book/author/Tolkien
        // =========================================================
        [HttpGet("author/{author:regex(^[[A-Za-z. ]]+$)}")]
        public IActionResult GetByAuthor(string author)
        {
            var books = BookData.Books.Where(b => b.Author
                        .Contains(author,
                        StringComparison.OrdinalIgnoreCase
            )).ToList();
            return books.Any()
                    ? Ok(books)
                    : NotFound($"No books found by author: {author}");
        }

        // ========================================================
        // MIN & MAX CONSTRAINT - Price range ($5 to $100)
        // URL: GET /api/book/price/10/to/20
        // =========================================================
        [HttpGet
        ("price/{minPrice:decimal:min(5)}/to/{maxPrice:decimal:max(100)}")]
        public IActionResult GetByPriceRange
        (decimal minPrice, decimal maxPrice)
        {
            if (minPrice > maxPrice)
            {
                return
                BadRequest("Minimum price cannot exceed maximum price");
            }
            var books = BookData.Books
                .Where(b => b.Price >= minPrice && b.Price <= maxPrice)
                .ToList();
            return books.Any()
                ? Ok(books)
    : NotFound($"No books found between ${minPrice} and ${maxPrice}");
        }

        // ============================================================
        // RANGE CONSTRAINT - Year must be between 1800 and 2025
        // URL: GET /api/book/year/1937
        // ============================================================
        [HttpGet("year/{year:int:range(1800,2025)}")]
        public IActionResult GetByYear(int year)
        {
            var books = BookData.Books
                .Where(b => b.PublishedYear == year).ToList();
            return books.Any()
                ? Ok(books)
                : NotFound($"No books found published in {year}");
        }

        // ============================================================
        // MULTIPLE CONSTRAINTS - Genre + Rating combination
        // URL: GET /api/book/filter/fantasy/5
        // ============================================================
        [HttpGet("filter/{genre:alpha}/{rating:int:range(1,5)}")]
        public IActionResult GetByGenreAndRating(string genre, int rating)
        {
            var books = BookData.Books
                .Where(b => b.Genre.Equals(genre,
                StringComparison.OrdinalIgnoreCase)
                         && b.Rating == rating)
                .ToList();
            return books.Any()
                ? Ok(books)
            : NotFound($"No {genre} books found with rating {rating}");
        }

         // =====================================================
        // CUSTOM CONSTRAINT - Only letters allowed some specific genres
        // URL: GET /api/book/strict-genre/fantasy
        // Invalid: /api/book/strict-genre/toys --> 404 (not allowed in list)
        // =====================================================
        [HttpGet("strict-genre/{genre:allowedGenres}")]
        public IActionResult GetByStrictGenre(string genre)
        {
            var books = BookData.Books
                .Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Ok(books);
        }
    }
}


