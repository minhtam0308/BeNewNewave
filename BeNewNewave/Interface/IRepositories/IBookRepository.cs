using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepo;

namespace BeNewNewave.Interface.IRepositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        List<BookResponse>? GetAllBook();
        Task<List<BookResponse>?> GetBookPaginate(PaginationRequest paginationRequest);
        Task<int> GetCountBook(PaginationRequest paginationRequest);
        Book? GetBookByIdIncludeAuthor(Guid id);
    }
}
