namespace BeNewNewave.Entities
{
    public class DetailBorrow : BaseEntity
    {
        public Guid IdBorrow { get; set; }
        public Borrow? Borrow { get; set; }
        public Guid IdBook { get; set; }
        public Book? Book { get; set; }
        public int Quantity { get; set; } = 0;

    }
}
