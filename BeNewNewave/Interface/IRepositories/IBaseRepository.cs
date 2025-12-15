namespace BeNewNewave.Interface.IRepo
{
    public interface IBaseRepository<T> where T : class
    {
        T? GetById(object id);
        IEnumerable<T> GetAll();
        void Insert(T entity, string idUser);
        void Update(T entity, string idUser);
        void Delete(object idEntity, string idUser);
        int SaveChanges();
    }
}
