
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepo;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.Services;
using BeNewNewave.Services;
using Microsoft.EntityFrameworkCore;


namespace BeNewNewave.Sevices
{
    public class UserService : BaseService<User>,IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ResponseDto _response = new ResponseDto();
        public UserService(IUserRepository repo) : base(repo)
        {
            _userRepository = repo;
        }

        public ResponseDto PutChangeUser(UserResponse request)
        {

            var user = _userRepository.GetById(request.Id);
            if (user == null)
            {
                return _response.GenerateStrategyResponseDto("userError");
            }
            user.Name = request.Name;
            user.Location = request.Location;
            user.Age = request.Age;
            user.Department = request.Department;
            user.Class = request.Class;
            user.PhoneNumber = request.PhoneNumber;
            user.UrlUserImage = request.UrlUserImage;

            _userRepository.SaveChanges();
            return _response.GenerateStrategyResponseDto("success");
        }

    }
}
