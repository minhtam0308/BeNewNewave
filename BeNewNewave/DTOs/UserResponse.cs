namespace BeNewNewave.DTOs
{
    public class UserResponse
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Role { get; set; }
        public required string UrlUserImage { get; set; } = "258d5e1a-ff57-4092-2a5d-08de2e43c05d";
        public int? Age { get; set; } = null;
        public string? Location { get; set; } = string.Empty;
        public string? Department { get; set; } = string.Empty;
        public string? Class { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
    }
}
