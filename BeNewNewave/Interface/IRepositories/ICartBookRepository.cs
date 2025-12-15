using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepo;

namespace BeNewNewave.Interface.IRepositories
{
    public interface ICartBookRepository : IBaseRepository<CartBook>
    {
        CartBook? GetByIdBookAndIdCart(Guid idCart, Guid idBook);
    }
}
