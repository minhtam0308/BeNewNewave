
using BeNewNewave.DTOs;
using BeNewNewave.Entities;

namespace BeNewNewave.Interface.IServices
{
    public interface IBookServices : IBaseService<Book>
    {
        List<BookResponse>? GetAllBook();
        Task<ResponseDto?> GetBookPaginateAsync(PaginationRequest paginationRequest);
        ResponseDto PostCreateBook(BookRequest request, string idUser);
        ResponseDto PutBook(Book request, string idUser);
        //Task<int> DelBook(Guid idBook);

    }
}
