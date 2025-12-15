using BeNewNewave.DTOs;
using BeNewNewave.Entities;

namespace BeNewNewave.Interface.IServices
{
    public interface ICartServices : IBaseService<Cart>
    {
        ResponseDto PostAddCart(AddBookToCartRequest request, Guid idUser);
        ResponseDto GetAllCart(Guid idUser);
    }
}
