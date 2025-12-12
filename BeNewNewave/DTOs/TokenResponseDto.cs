using BeNewNewave.DTOs;

namespace BeNewNewave.DTOs
{
    public class TokenResponseDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required UserResponse User { get; set; }

    }
}
