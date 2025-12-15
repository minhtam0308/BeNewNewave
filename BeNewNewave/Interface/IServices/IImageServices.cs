
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IServices;

namespace BeNewNewave.Interface.Services
{
    public interface IImageServices : IBaseService<BookImage>
    {
        ResponseDto UpdateImage(BookImage entity, string idUser);
        //Task<Guid?> PostAddImage(ImageRequest request);
        //Task<BookImage?> GetBookImage(Guid idImage);
        //Task<int?> PutImage(Guid id, byte[] image);
        //Task<int?> DelImage(Guid id);
        //Task<byte[]?> GetImageGeneral();
    }
}
