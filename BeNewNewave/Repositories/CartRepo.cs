using BeNewNewave.Data;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BeNewNewave.Repositories
{
    public class CartRepo : BaseRepository<Cart>, ICartRepository
    {
        public CartRepo(AppDbContext context) : base(context) {}

        public Cart? GetByIdUser(Guid idUser)
        {
            return _dbSet.FirstOrDefault(c => c.IdUser == idUser);
        }
        public Cart? GetCartDetail(Guid idUser)
        {
            return _dbSet.Include(c => c.CartBooks).FirstOrDefault(c => c.IdUser == idUser);
        }
    }
}
