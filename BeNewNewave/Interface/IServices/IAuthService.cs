
using BeNewNewave.DTOs;

namespace BeNewNewave.Interface.Service
{
    public interface IAuthService
    {
        Task<ResponseDto> Register(UserDto request);
        Task<ResponseDto> LoginAsyn(UserLoginDto request);
        Task<ResponseDto> RefreshTokenAsyn(RefreshTokenRequest request);

    }
}
