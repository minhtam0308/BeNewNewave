using BeNewNewave.Entities;
using BeNewNewave.DTOs;


namespace BeNewNewave.Interface.IServices
{
    public interface IAuthorServices : IBaseService<Author>
    {
        ResponseDto EditAuthor(AuthorRenameRequest author, string idUser);
        ResponseDto DeleteAuthor(Guid idEntity, string idUser) ;
    }
}
