
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.Services;
using BeNewNewave.Services;
using Serilog;

namespace Backend.Sevices
{
    public class ImageServices : BaseService<BookImage>, IImageServices
    {
        private readonly IImageRepository _imageRepository;
        private readonly ResponseDto _response = new ResponseDto();
        public ImageServices(IImageRepository imageRepository) : base(imageRepository) 
        {
            _imageRepository = imageRepository;
        }
        //public async Task<BookImage?> GetBookImage(Guid idImage)
        //{
        //    try
        //    {
        //        var image = await context.BookImages
        //                  .FirstOrDefaultAsync(x => x.Id == idImage);
        //        return image;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Information("Error GetBookImage {t}", ex);
        //        return null;
        //    }
        //}

        //public async Task<Guid?> PostAddImage(ImageRequest request)
        //{
        //    try
        //    {
        //        BookImage newImage = new BookImage() { Image = request.Image };
        //        _repo.Insert(newImage);

        //        return newImage.Id;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Information("Error PostAddImage {t}", ex);
        //        return null;
        //    }
        //}

        public ResponseDto UpdateImage(BookImage entity, string idUser)
        {
            var editImage = _imageRepository.GetById(entity.Id);
            if (editImage == null)
            {
                return _response.GenerateStrategyResponseDto("userError");
            }
            editImage.Image = entity.Image;
            editImage.UpdatedAt = DateTime.UtcNow;
            editImage.UpdatedBy = idUser;
            _imageRepository.SaveChanges();
            return _response.GenerateStrategyResponseDto("success");

        }

        //public async Task<int?> DelImage(Guid idImage)
        //{
        //    try
        //    {
        //        var image = await context.BookImages
        //                  .FirstOrDefaultAsync(x => x.Id == idImage);
        //        if (image == null)
        //        {
        //            return 2;
        //        }
        //        context.BookImages.Remove(image);
        //        context.SaveChanges(); 
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Information("Error GetBookImage {t}", ex);
        //        return 1;
        //    }
        //}

        //public async Task<byte[]?> GetImageGeneral()
        //{
        //    try
        //    {
        //        var image = await context.BookImages
        //                  .FirstOrDefaultAsync(x => x.Id == Guid.Parse("258d5e1a-ff57-4092-2a5d-08de2e43c05d"));
        //        if (image == null)
        //        {
        //            return null;
        //        }
        //        return image.Image;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Information("Error GetBookImage {t}", ex);
        //        return null;
        //    }
        //}
    }
}
