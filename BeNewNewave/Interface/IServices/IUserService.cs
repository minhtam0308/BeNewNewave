
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IServices;
namespace BeNewNewave.Interface.Services
{
    public interface IUserService : IBaseService<User>
    {
        ResponseDto PutChangeUser(UserResponse request);

    }
}
