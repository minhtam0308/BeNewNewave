
using BeNewNewave.DTOs;
using BeNewNewave.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace BeNewNewave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly ResponseDto _response = new ResponseDto();
        [Authorize(Roles = "admin, user")]
        [HttpPut("putChangeUser")]
        public async Task<ActionResult<ResponseDto>> PutChangUser(UserResponse request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid userIdReuslt))
                return _response.GenerateStrategyResponseDto("success");
            request.Id = userIdReuslt;
            var resultChange = userService.PutChangeUser(request);
            return Ok(resultChange);
        }
    }
}
