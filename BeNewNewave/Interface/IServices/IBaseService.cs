namespace BeNewNewave.Interface.IServices
{
    public interface IBaseService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(object id);
        void Create(T entity, string idUser);
        void Update(T entity, string idUser);
        void Delete(object idEntity, string idUser);
    }
}
