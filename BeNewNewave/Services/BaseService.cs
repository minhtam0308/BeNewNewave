using BeNewNewave.Interface.IRepo;
using BeNewNewave.Interface.IServices;

namespace BeNewNewave.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IBaseRepository<T> _repo;

        public BaseService(IBaseRepository<T> repo)
        {
            _repo = repo;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repo.GetAll();
        }

        public virtual T? GetById(object id)
        {
            return _repo.GetById(id);
        }

        public virtual void Create(T entity)
        {
            _repo.Insert(entity);
            _repo.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _repo.Update(entity);
            _repo.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            _repo.Delete(id);
            _repo.SaveChanges();
        }
    }
}
