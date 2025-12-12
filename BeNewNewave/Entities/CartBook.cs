namespace BeNewNewave.Entities
{
    public class CartBook: BaseEntity
    {
        public Guid IdCart { get; set; }
        public Cart? Cart { get; set; }
        public Guid IdBook { get; set; }
        public Book? Book { get; set; }

        public int Quantity { get; set; } = 1;

    }
}
