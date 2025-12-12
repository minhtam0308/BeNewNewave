namespace BeNewNewave.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }=String.Empty;
        public string Name { get; set; }=String.Empty;
        public int? Age { get; set; }
        public string? Location { get; set; }
        public string? Department { get; set; }
        public string? Class { get; set; }
        public string? PhoneNumber { get; set; }
        public string PasswordHash { get; set; } = String.Empty;
        public string Role { get; set; } = "user";
        public string? RefreshToken {  get; set; }
        public string? UrlUserImage { get; set; } = "258d5e1a-ff57-4092-2a5d-08de2e43c05d";
        public DateTime? RefreshTokeExpiryTime { get; set; }

        public List<Borrow> Users { get; set; } = new();
        public List<Borrow> Admins { get; set; } = new();
        public Cart? Cart { get; set; }
    }
}
