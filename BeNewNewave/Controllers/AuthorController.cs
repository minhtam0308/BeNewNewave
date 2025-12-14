
using Azure;
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.Service;
using BeNewNewave.Strategy.ResponseDtoStrategy;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController(IAuthorServices authorServices) : ControllerBase
    {

        private readonly ResponseDto _responseDto = new ResponseDto();

        [Authorize(Roles = "admin")]
        [HttpPost("postCreateAuthor")]
        public async Task<ActionResult> PostCreateAuthor(string? nameAuthor)
        {
            if (nameAuthor == null || nameAuthor == "")
            {
                return BadRequest(GenerateStrategyResponseDto("userError"));
            }
            var newAuthor = new Author() { NameAuthor = nameAuthor };
            authorServices.Create(newAuthor);
            return Ok(GenerateStrategyResponseDto("success"));
        }


        [HttpGet("getAllAuthor")]
        public async Task<ActionResult> GetAllAuthor()
        {
            var result = authorServices.GetAll();
            _responseDto.SetResponseDtoStrategy(new Success("get author success", result ));
            
            return Ok(_responseDto.GetResponseDto());
        }

        [Authorize(Roles = "admin")]
        [HttpPut("putEditAuthor")]
        /// edit author by id
        public async Task<ActionResult<ResponseDto>> PutEditAuthor([FromBody] AuthorRenameRequest author)
        {
            //check author null
            if (author is null || author.NameAuthor == "")
                return BadRequest(GenerateStrategyResponseDto("userError"));

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return BadRequest(GenerateStrategyResponseDto("userError"));
            return authorServices.EditAuthor(author, userIdGuid);

        }

        [Authorize(Roles = "admin")]
        [HttpDelete("deleteAuthor")]
        public async Task<ActionResult<ResponseDto>> DeleteAuthor(Guid idAuthor)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            return authorServices.DeleteAuthor(idAuthor, userId);
        }

        private ResponseDto GenerateStrategyResponseDto(string result)
        {
            switch (result)
            {
                case "userError":
                    _responseDto.SetResponseDtoStrategy(new UserError());
                    return _responseDto.GetResponseDto();
                case "serverError":
                    _responseDto.SetResponseDtoStrategy(new ServerError());
                    return _responseDto.GetResponseDto();
                default:
                    _responseDto.SetResponseDtoStrategy(new Success());
                    return _responseDto.GetResponseDto();
            }
        }

    }
}
