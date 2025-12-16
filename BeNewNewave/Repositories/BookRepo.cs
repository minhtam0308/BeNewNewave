using AutoMapper;
using BeNewNewave.Data;
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Strategy.ResponseDtoStrategy;
using Microsoft.EntityFrameworkCore;
using System;

namespace BeNewNewave.Repositories
{
    public class BookRepo : BaseRepository<Book>, IBookRepository
    {
        private readonly IMapper _mapper; 

        public BookRepo(AppDbContext context, IMapper mapper) : base(context) 
        { 
            _mapper = mapper;
        }

        public List<BookResponse>? GetAllBook()
        {
            List<Book> listBook = _dbSet.Include(b => b.Author).ToList();   
            List<BookResponse> lstResponse = _mapper.Map<List<BookResponse>>(listBook);
            return lstResponse;
        }

        public Book? GetBookByIdIncludeAuthor(Guid id)
        {
            var book = _dbSet.Include(b => b.Author).FirstOrDefault(b=> b.Id == id);
           
            return book;
        }

        public async Task<List<BookResponse>?> GetBookPaginate(PaginationRequest paginationRequest)
        {

            var items = await  _dbSet
                .Include(b => b.Author)
                .OrderBy(x => x.Id)
                .Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
                .Take(paginationRequest.PageSize)
                .ToListAsync();
            List<BookResponse> lstResponse = _mapper.Map<List<BookResponse>>(items);

            return lstResponse;
        }

        public async Task<int> GetCountBook(PaginationRequest paginationRequest)
        {
            return await _dbSet.CountAsync();
        }
    }
}
