namespace LibraryApi5.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty; // Fantasy, Mystery, etc.
        public int Rating { get; set; }                   // 1 to 5 stars
        public string ISBN { get; set; } = string.Empty;  // 978-0-547-92822-7 format
        public int PublishedYear { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
    }
}

