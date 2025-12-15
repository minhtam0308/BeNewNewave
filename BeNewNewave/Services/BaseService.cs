using BeNewNewave.Interface.IRepo;
using BeNewNewave.Interface.IServices;

namespace BeNewNewave.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _repo;

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

        public virtual void Create(T entity, string idUser)
        {
            _repo.Insert(entity, idUser);
            _repo.SaveChanges();
        }

        public virtual void Update(T entity, string idUser)
        {
            _repo.Update(entity, idUser);
            _repo.SaveChanges();
        }

        public virtual void Delete(object idEntity, string idUser)
        {
            _repo.Delete( idEntity, idUser);
            _repo.SaveChanges();
        }
    }
}
