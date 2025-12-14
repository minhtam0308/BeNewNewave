namespace BeNewNewave.Interface.IServices
{
    public interface IBaseService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(object id);
        void Create(T entity);
        void Update(T entity);
        void Delete(object idEntity, string idUser);
    }
}
