using BeNewNewave.Data;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;

namespace BeNewNewave.Repositories
{
    public class UserRepo : BaseRepository<User>, IUserRepository
    {
        public UserRepo(AppDbContext context) : base(context) {}
    }
}
