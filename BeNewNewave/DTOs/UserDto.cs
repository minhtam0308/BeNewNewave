namespace BeNewNewave.DTOs
{
    public class UserDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
