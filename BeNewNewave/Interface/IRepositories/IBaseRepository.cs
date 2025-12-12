namespace BeNewNewave.Interface.IRepo
{
    public interface IBaseRepository<T> where T : class
    {
        T? GetById(object id);
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
        int SaveChanges();
    }
}
