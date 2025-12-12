namespace BeNewNewave.Entities
{
    public class Borrow : BaseEntity
    {
        public List<DetailBorrow> DetailBorrow { get; set; } = new();
        public Guid IdAdmin { get; set; }
        public User? Admin { get; set; }
        public Guid IdUser { get; set; }
        public User? User { get; set; }
        public required DateTime ExpiresBorrow { get; set; }
        public DateTime? RealTimeBorrow { get; set; }
        public DateTime? ExpiresReturn { get; set; }
        public DateTime? RealTimeReturn { get; set; }

    }
}
