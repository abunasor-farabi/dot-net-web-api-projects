namespace LibraryApi1.Models
{
    public class BookSearchFilter
    {
        public string? Genre { get; set; }
        public string? Author { get; set; }
        public bool? IsAvailable { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}

