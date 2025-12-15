
using BeNewNewave.DTOs;
using BeNewNewave.Interface.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeNewNewave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartServices cartServices) : ControllerBase
    {
        private readonly ResponseDto _responseDto = new ResponseDto();
        [Authorize(Roles = "user, admin")]
        [HttpPost("addBookToCart")]
        public ActionResult AddBookToCart(AddBookToCartRequest request)
        {

            if (request.IdBook is null)
                return BadRequest(_responseDto.GenerateStrategyResponseDto("userError"));

            if (request.Quantity <= 0)
                return BadRequest(_responseDto.GenerateStrategyResponseDto("userError"));

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid userIdReuslt))
                return BadRequest(_responseDto.GenerateStrategyResponseDto("userError"));

            var resultBorrow = cartServices.PostAddCart(request, userIdReuslt);

            return Ok(resultBorrow);
        }


        [Authorize(Roles = "user, admin")]
        [HttpGet("getAllCart")]
        public ActionResult GetAllCart()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid userIdReuslt))
                return BadRequest(_responseDto.GenerateStrategyResponseDto("userError"));

            var resultGetCart = cartServices.GetAllCart(userIdReuslt);

            return Ok(resultGetCart);
        }
    }
}
