using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepo;

namespace BeNewNewave.Interface.IRepositories
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Cart? GetByIdUser(Guid id);
        Cart? GetCartDetail(Guid idUser);
    }
}
