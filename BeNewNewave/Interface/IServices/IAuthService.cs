
using BeNewNewave.DTOs;

namespace BeNewNewave.Interface.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto> RegisterAsync(UserDto request);
        Task<ResponseDto> LoginAsyn(UserLoginDto request);
        Task<ResponseDto> RefreshTokenAsyn(RefreshTokenRequest request);

    }
}
