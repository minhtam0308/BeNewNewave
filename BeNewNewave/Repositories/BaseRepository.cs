using BeNewNewave.Data;
using BeNewNewave.Interface.IRepo;
using Microsoft.EntityFrameworkCore;

namespace BeNewNewave.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
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

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }
        public virtual int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
