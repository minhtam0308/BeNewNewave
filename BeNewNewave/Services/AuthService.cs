using BeNewNewave.Data;
using BeNewNewave.Entities;
using BeNewNewave.Interface.Service;
using BeNewNewave.DTOs;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Strategy.ResponseDtoStrategy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BeNewNewave.Sevices
{
    public class AuthService(AppDbContext context, IConfiguration configuration, IUserRepository userRepo) : IAuthService
    {
        private ResponseDto _response = new ResponseDto();


        public async Task<ResponseDto> LoginAsyn(UserLoginDto request)
        {
            if (!IsValidPassword(request.Password) || !IsValidEmail(request.Email))
            {
                _response.SetResponseDtoStrategy(new UserError());
                return _response.GetResponseDto();
            }
            var user = userRepo.FindUserByEmail(request.Email);
            if (user is null)
            {
                _response.SetResponseDtoStrategy(new UserError("User is not exist"));
                return _response.GetResponseDto();
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                _response.SetResponseDtoStrategy(new UserError("Email or password is not valid"));
                return _response.GetResponseDto();
            }
            _response.SetResponseDtoStrategy(new Success("Login success", await CreateTokenRespon(user)));
            return _response.GetResponseDto();
        }

        private async Task<TokenResponseDto> CreateTokenRespon(User user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsyn(user),
                User = new UserResponse { Email = user.Email , 
                    Id=user.Id, 
                    Role= user.Role, 
                    Name=user.Name, 
                    UrlUserImage = user.UrlUserImage!,
                    Age = user.Age,
                    Location = user.Location,
                    Department = user.Department,
                    Class = user.Class,
                    PhoneNumber = user.PhoneNumber
                }
            };
        }

        public async Task<ResponseDto> Register(UserDto request)
        {
            if(!IsValidPassword(request.Password) || !IsValidEmail(request.Email))
            {
                _response.SetResponseDtoStrategy(new UserError());
                return _response.GetResponseDto();
            }
            if (userRepo.IsEmailExist(request.Email)) {
                _response.SetResponseDtoStrategy(new UserError("Email was exist!"));
                return _response.GetResponseDto();
            }
            var user = new User();
            var passwordHard = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.Email = request.Email;
            user.PasswordHash = passwordHard;
            user.Name = request.Name;
            userRepo.Insert(user);
            userRepo.SaveChanges();
            return GenerateStrategyResponseDto("success");
        }

        bool IsValidPassword(string password)
        {
            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$";
            return Regex.IsMatch(password, pattern);
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            return emailRegex.IsMatch(email);
        }



        private String CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["appsetting:token"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration["appsetting:issuer"],
                audience: configuration["appsetting:audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(configuration.GetValue<int>("appsetting:addMinu")),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsyn(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokeExpiryTime = DateTime.UtcNow.AddDays(configuration.GetValue<int>("appsetting:timeResfreshTokenExpire"));
            await context.SaveChangesAsync();
            return refreshToken;
        }


        public async Task<ResponseDto> RefreshTokenAsyn(RefreshTokenRequest request)
        {
            var user = userRepo.GetById(request.UserId);
            if (user is null || user.RefreshTokeExpiryTime < DateTime.UtcNow || user.RefreshToken != request.RefreshToken)
            {
                _response.SetResponseDtoStrategy(new UserError());
                return _response.GetResponseDto();
            }
            _response.SetResponseDtoStrategy(new Success("Refresh success", await CreateTokenRespon(user)));
            return _response.GetResponseDto();
        }

        private ResponseDto GenerateStrategyResponseDto(string result)
        {
            switch (result)
            {
                case "userError":
                    _response.SetResponseDtoStrategy(new UserError());
                    return _response.GetResponseDto();
                case "serverError":
                    _response.SetResponseDtoStrategy(new ServerError());
                    return _response.GetResponseDto();
                default:
                    _response.SetResponseDtoStrategy(new Success());
                    return _response.GetResponseDto();
            }
        }

    }
}
