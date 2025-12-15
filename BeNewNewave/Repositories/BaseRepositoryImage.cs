using BeNewNewave.Data;
using BeNewNewave.Interface.IRepo;
using Microsoft.EntityFrameworkCore;

namespace BeNewNewave.Repositories
{
    public class BaseRepositoryImage<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        protected readonly ImageDBContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepositoryImage(ImageDBContext context)
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

        public virtual void Insert(T entity, string idUser)
        {
            entity.CreatedBy = idUser;
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity, string idUser)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = idUser;
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

}
