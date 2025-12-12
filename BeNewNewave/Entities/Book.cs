using System.ComponentModel.DataAnnotations;

namespace BeNewNewave.Entities
{
    public class Book : BaseEntity
    {
        public required string Title { get; set; }
        public Guid IdAuthor { get; set; }
        public Author? Author { get; set; }

        public string? Description { get; set; }

        public int TotalCopies { get; set; }       
        public int AvailableCopies { get; set; }   

        public string? UrlBook { get; set; }     

        public List<CartBook>? CartBooks { get; set; }
        public List<DetailBorrow>? DetailBorrows { get; set; }
    }
}
