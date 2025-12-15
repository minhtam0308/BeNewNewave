using Azure.Core;
using BeNewNewave.Data;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace BeNewNewave.Repositories
{
    public class UserRepo : BaseRepository<User>, IUserRepository
    {
        public UserRepo(AppDbContext context) : base(context) { }

        public User? FindUserByEmail(string email)
        {
            return _dbSet.FirstOrDefault(u => u.Email == email);
        }

        public bool IsEmailExist(string email)
        {
            return _dbSet.Any(u => u.Email == email);
        }
        public void Insert(User entity)
        {
            entity.CreatedAt = DateTime.Now;
            _dbSet.Add(entity);
        }
    }
}
