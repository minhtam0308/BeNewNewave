
using Azure;
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IServices;
using BeNewNewave.Strategy.ResponseDtoStrategy;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BeNewNewave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController(IAuthorServices authorServices) : ControllerBase
    {

        private readonly ResponseDto _responseDto = new ResponseDto();

        [Authorize(Roles = "admin")]
        [HttpPost("postCreateAuthor")]
        public ActionResult PostCreateAuthor(string? nameAuthor)
        {
            if (nameAuthor == null || nameAuthor == "")
            {
                return BadRequest(_responseDto.GenerateStrategyResponseDto("userError"));
            }
            var newAuthor = new Author() { NameAuthor = nameAuthor };
            //get id user
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return BadRequest(_responseDto.GenerateStrategyResponseDto("userError"));
            authorServices.Create(newAuthor, userId);
            return Ok(_responseDto.GenerateStrategyResponseDto("success"));
        }


        [HttpGet("getAllAuthor")]
        public ActionResult GetAllAuthor()
        {
            var result = authorServices.GetAll();
            _responseDto.SetResponseDtoStrategy(new Success("get author success", result ));
            
            return Ok(_responseDto.GetResponseDto());
        }

        [Authorize(Roles = "admin")]
        [HttpPut("putEditAuthor")]
        /// edit author by id
        public ActionResult<ResponseDto> PutEditAuthor([FromBody] AuthorRenameRequest author)
        {
            //check author null
            if (author is null || author.NameAuthor == "")
                return BadRequest(_responseDto.GenerateStrategyResponseDto("userError"));

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return BadRequest(_responseDto.GenerateStrategyResponseDto("userError"));
            return authorServices.EditAuthor(author, userId);

        }

        [Authorize(Roles = "admin")]
        [HttpDelete("deleteAuthor")]
        public ActionResult<ResponseDto> DeleteAuthor(Guid idAuthor)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            return authorServices.DeleteAuthor(idAuthor, userId);
        }



    }
}
