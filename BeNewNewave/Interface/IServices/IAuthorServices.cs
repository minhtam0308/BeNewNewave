using BeNewNewave.Entities;
using BeNewNewave.DTOs;
using BeNewNewave.Interface.IServices;


namespace BeNewNewave.Interface.Service
{
    public interface IAuthorServices : IBaseService<Author>
    {
        ResponseDto EditAuthor(AuthorRenameRequest author, Guid id);
        ResponseDto DeleteAuthor(Guid idEntity, string idUser) ;
    }
}
