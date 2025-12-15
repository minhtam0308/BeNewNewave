namespace BeNewNewave.DTOs
{
    public class BookRequest
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public Guid IdAuthor { get; set; }
        public int TotalCopies { get; set; }
        public string? UrlBook { get; set; }
    }
}
