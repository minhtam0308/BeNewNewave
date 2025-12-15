using BeNewNewave.Data;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BeNewNewave.Repositories
{
    public class CartBookRepo : BaseRepository<CartBook>, ICartBookRepository
    {
        public CartBookRepo(AppDbContext context) : base(context) { }

        public CartBook? GetByIdBookAndIdCart(Guid idCart, Guid idBook)
        {
            return _dbSet.FirstOrDefault(c => c.IdCart == idCart && c.IdBook == idBook);
        }
    }
}
