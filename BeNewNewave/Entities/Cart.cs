namespace BeNewNewave.Entities
{
    public class Cart : BaseEntity
    {
        public Guid IdUser { get; set; }
        public User? User { get; set; }
        public List<CartBook>? CartBooks { get; set; } 
    }
}
