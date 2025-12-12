using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepo;

namespace BeNewNewave.Interface.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        bool IsEmailExist(string email);
        User? FindUserByEmail(string email);
    }
}
