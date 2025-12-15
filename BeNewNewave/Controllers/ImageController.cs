
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.Services;
using BeNewNewave.Strategy.ResponseDtoStrategy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController(IImageServices imageServices) : ControllerBase
    {
        private readonly ResponseDto _response = new ResponseDto();
        [Authorize(Roles = "admin, user")]
        [HttpPost("postCreateImage")]
        public async Task<ActionResult<Guid>> PostCreateImage(IFormFile file)
        {
            //check data
            if (file is null || file.Length == 0)
            {
                return BadRequest(_response.GenerateStrategyResponseDto("userError"));
            }
            //get iduser
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return BadRequest(_response.GenerateStrategyResponseDto("userError"));
            //convert file to byte
            using var tempMemory = new MemoryStream();
            await file.CopyToAsync(tempMemory);
            var imageByte = tempMemory.ToArray();
            var newImage = new BookImage() { Image = imageByte };
            //create image
            imageServices.Create(newImage, userId);
            //return idImage
            _response.SetResponseDtoStrategy(new Success("get image success", newImage.Id));
            return Ok(_response.GetResponseDto());
        }

        [HttpGet("getImage")]
        public ActionResult? GetImage(string idImage)
        {
            if (!Guid.TryParse(idImage, out var guidId))
            {
                _response.SetResponseDtoStrategy(new UserError());
                return BadRequest(_response.GetResponseDto());
            }
            var result = imageServices.GetById(guidId);
            if(result == null)
            {
                return null; 
            }
            return File(result.Image, "image/png");
        }

        [Authorize(Roles = "admin")]
        [HttpPut("putImage")]
        public async Task<ActionResult> PutImage(IFormFile file, [FromForm] string idImage)
        {
            //check data
            if (file is null || file.Length == 0)
            {
                return BadRequest(_response.GenerateStrategyResponseDto("userError"));
            }
            if (!Guid.TryParse(idImage, out var guidIdImage))
            {
                return BadRequest(_response.GenerateStrategyResponseDto("userError"));
            }

            //get idUser
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return BadRequest(_response.GenerateStrategyResponseDto("userError"));

            using var tempMemory = new MemoryStream();
            await file.CopyToAsync(tempMemory);
            var imageByte = tempMemory.ToArray();

            var resultPutImage = imageServices.UpdateImage(new BookImage() { Id = guidIdImage, Image= imageByte}, userId);
            return Ok(resultPutImage);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("deleteImage")]
        public async Task<ActionResult> DelImage(string idImage)
        {

            if (!Guid.TryParse(idImage, out var guidId))
            {
                return BadRequest(_response.GenerateStrategyResponseDto("userError"));
            }

            //get idUser
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userId, out Guid userIdGuid))
                return BadRequest(_response.GenerateStrategyResponseDto("userError"));

            imageServices.Delete(guidId, userId);
            return Ok(_response.GenerateStrategyResponseDto("success"));
        }
    }
}
