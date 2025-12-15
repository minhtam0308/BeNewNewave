
using AutoMapper;
using Azure.Core;
using BeNewNewave.Data;
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepo;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.IServices;
using BeNewNewave.Services;
using BeNewNewave.Strategy.ResponseDtoStrategy;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Backend.Sevices
{
    public class BookServices : BaseService<Book>, IBookServices
    {
        private IBookRepository _bookRepository;
        private readonly ResponseDto _response = new ResponseDto();
        private readonly IMapper _mapper;
        public BookServices(IBookRepository bookRepository, IMapper mapper) : base(bookRepository)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }


        //public async Task<int> DelBook(Guid idBook)
        //{
        //    try
        //    {

        //        var bookDel = context.Books.FirstOrDefault(b => b.Id == idBook);
        //        if (bookDel == null)
        //        {
        //            return 2;
        //        }
        //        context.Books.Remove(bookDel);
        //        await context.SaveChangesAsync();
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Information("Error PutBook {t}", ex);

        //        return 1;
        //    }

        //}

        public List<BookResponse>? GetAllBook()
        {
            List<BookResponse>? books = _bookRepository.GetAllBook();
            return books;   
        }

        public async Task<ResponseDto?> GetBookPaginateAsync(PaginationRequest paginationRequest)
        {
            //validate
            int pageNumber = paginationRequest.PageNumber < 1 ? 1 : paginationRequest.PageNumber;
            int pageSize = paginationRequest.PageSize < 1 ? 6 : paginationRequest.PageSize;

            paginationRequest.PageNumber = pageNumber;
            paginationRequest.PageSize = pageSize;

            var totalCount = await _bookRepository.GetCountBook(paginationRequest);

            if ((paginationRequest.PageNumber - 1) * paginationRequest.PageSize > totalCount)
            {
                return null;
            }
            var items = await _bookRepository.GetBookPaginate(paginationRequest);

                var result = new PagedResult()
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageNumber = paginationRequest.PageNumber,
                    PageSize = paginationRequest.PageSize
                };
            _response.SetResponseDtoStrategy(new Success("get success", result));
            return _response.GetResponseDto();
        }


        public ResponseDto PostCreateBook(BookRequest request, string idUser)
        {
            Book bookNew = _mapper.Map<Book>(request);
            _bookRepository.Insert(bookNew, idUser);
            _bookRepository.SaveChanges();
            return _response.GenerateStrategyResponseDto("success");

        }

        public ResponseDto PutBook(Book request, string idUser)
        {

            var bookEdit = _bookRepository.GetById(request.Id);
            if (bookEdit == null)
            {
                return _response.GenerateStrategyResponseDto("userError");
            }
            bookEdit.Title = request.Title;
            bookEdit.IdAuthor = request.IdAuthor;
            bookEdit.Description = request.Description;
            bookEdit.AvailableCopies = request.AvailableCopies;
            bookEdit.TotalCopies = request.TotalCopies;
            bookEdit.UrlBook = request.UrlBook;
            bookEdit.UpdatedBy = idUser;
            _bookRepository.SaveChanges();
            return _response.GenerateStrategyResponseDto("success");


        }
    }
}
