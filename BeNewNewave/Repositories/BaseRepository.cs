using BeNewNewave.Data;
using BeNewNewave.Interface.IRepo;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BeNewNewave.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T? GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Delete(object idEntity, string idUser)
        {
            var entity = _dbSet.Find(idEntity);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.UpdatedBy = idUser;
            }
        }
        public virtual int SaveChanges()
        {
            return _context.SaveChanges();
        }

    }


    public interface IBaseEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
