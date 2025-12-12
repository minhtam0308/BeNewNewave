
using BeNewNewave.DTOs;
using BeNewNewave.Interface.Service;
using BeNewNewave.DTOs;
using BeNewNewave.Strategy.ResponseDtoStrategy;
using Microsoft.AspNetCore.Mvc;


namespace BeNewNewave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IConfiguration configuration) : ControllerBase
    {
        private ResponseDto _response = new ResponseDto();
        [HttpPost("register")]
        public async Task<ActionResult<object>> Register(UserDto request)
        {
            if (request == null || request.Email == "" || request.Password == "" || request.Name == "" )
            {
                SetResponseUserError();
                return BadRequest(_response.GetResponseDto());
            }
            var user = await authService.Register(request);
            return Ok(user);
        }




        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> Login(UserLoginDto request)
        {
            if (request == null || request.Email == "" || request.Password == "")
            {
                SetResponseUserError();
                return BadRequest(_response.GetResponseDto());
            }
            ResponseDto result = await authService.LoginAsyn(request);
            if (result.errorCode != 0)
            {
                return BadRequest(result);
            }
            TokenResponseDto tokenInfor =(TokenResponseDto) result.data;
            Response.Cookies.Append("refreshToken", tokenInfor.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("appsetting:timeResfreshTokenExpire"))
            });

            // 6. Lưu userId vào cookies
            Response.Cookies.Append("userId", tokenInfor.User.Id.ToString(), new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("appsetting:timeResfreshTokenExpire"))
            });
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ResponseDto>> RefreshToken()
        {
            if (!Request.Cookies.TryGetValue("userId", out string? userId) || !Request.Cookies.TryGetValue("refreshToken", out string? refreshToken))
            {
                SetResponseUserError();
                return BadRequest(_response.GetResponseDto());
            }

            if (!Guid.TryParse(userId, out var guidId))
            {
                SetResponseUserError();
                return BadRequest(_response.GetResponseDto());
            }
            ResponseDto result = await authService.RefreshTokenAsyn(new RefreshTokenRequest() { UserId = guidId, RefreshToken = refreshToken });
            if (result.errorCode != 0)
            {
                SetResponseUserError();
                return BadRequest(_response.GetResponseDto());
            }
            TokenResponseDto tokenInfor = (TokenResponseDto)result.data;
            Response.Cookies.Append("refreshToken", tokenInfor.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("appsetting:timeResfreshTokenExpire"))
            });

            // 6. Lưu userId vào cookies
            Response.Cookies.Append("userId", tokenInfor.User.Id.ToString(), new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(configuration.GetValue<int>("appsetting:timeResfreshTokenExpire"))
            });

            return Ok(result);
        }


        [HttpGet("logout")]
        public async Task<ActionResult<ResponseDto>> Logout()
        {

            Response.Cookies.Append("refreshToken", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1)
            });

            // 6. Lưu userId vào cookies
            Response.Cookies.Append("userId", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1)
            });
            _response.SetResponseDtoStrategy(new Success());
            return Ok(_response.GetResponseDto());
        }

        private void SetResponseUserError()
        {
            _response.SetResponseDtoStrategy(new UserError());
        }

    }
}
