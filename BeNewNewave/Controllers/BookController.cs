
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IServices;
using BeNewNewave.Strategy.ResponseDtoStrategy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace BeNewNewave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(IBookServices bookServices) : ControllerBase
    {

        private readonly ResponseDto _responseDto = new ResponseDto();
        [HttpGet("getAllBook")]
        public ActionResult<ResponseDto> GetAllBook()
        {
            var lstBook = bookServices.GetAllBook();
            _responseDto.SetResponseDtoStrategy(new Success("get success", lstBook));
            return Ok(_responseDto.GetResponseDto());
        }

        [HttpGet("getPagedBook")]
        public async Task<ActionResult> GetPagedBookAsync([FromQuery] PaginationRequest request)
        {
            var inforPage = await bookServices.GetBookPaginateAsync(request);
            return Ok(inforPage);
        }

        [HttpPost("postCreateBook")]
        [Authorize(Roles = "admin")]
        public ActionResult<ResponseDto> PostCreateBook(BookRequest request)
        {
            if (request is null || request.TotalCopies == 0 || request.Title == "")
            {
                return BadRequest(_responseDto.GenerateStrategyResponseDto("userError"));
            }
            //get idUser
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return BadRequest(_responseDto.GenerateStrategyResponseDto("userError"));
            //create book
            var resultCreate = bookServices.PostCreateBook(request, userId);
            return Ok(resultCreate);
        }

        [HttpPut("putBook")]
        [Authorize(Roles = "admin")]
        public ActionResult<ResponseDto> PutBook(Book request)
        {
            //get idUser
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return BadRequest(_responseDto.GenerateStrategyResponseDto("userError"));
            var resultPutBook = bookServices.PutBook(request, userId);
            return Ok(resultPutBook);
        }

        [HttpDelete("delBook")]
        [Authorize(Roles = "admin")]
        public ActionResult<ResponseDto> DelBook(string idBook)
        {
            if (!Guid.TryParse(idBook, out var guidId))
            {
                return BadRequest(_responseDto.GenerateStrategyResponseDto("userError"));
            }

            //get idUser
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return BadRequest(_responseDto.GenerateStrategyResponseDto("userError"));
            bookServices.Delete(guidId, userId);
            return Ok(_responseDto.GenerateStrategyResponseDto("success"));

        }

    }
}
