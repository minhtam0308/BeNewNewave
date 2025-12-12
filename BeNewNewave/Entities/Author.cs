using System.ComponentModel.DataAnnotations;

namespace BeNewNewave.Entities
{
    public class Author : BaseEntity
    {
        public string? NameAuthor { get; set; }
        public List<Book>? Books { get; set; }
    }
}
