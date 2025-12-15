namespace BeNewNewave.DTOs
{
    public class BookResponse
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? NameAuthor { get; set; }
        public string? Description { get; set; }
        public Guid IdAuthor { get; set; }
        public int TotalCopies { get; set; }      
        public int AvailableCopies { get; set; }  

        public string? UrlBook { get; set; }
        public int Quantity { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
