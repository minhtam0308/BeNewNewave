

using AutoMapper;
using Azure;
using BeNewNewave.Data;
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.IServices;
using BeNewNewave.Services;
using BeNewNewave.Strategy.ResponseDtoStrategy;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Backend.Sevices
{
    public class AuthorServices : BaseService<Author>, IAuthorServices
    {
        private readonly ResponseDto _responseDto = new ResponseDto();
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _authorRepository;

        public AuthorServices(IAuthorRepository authorRepo, IMapper mapper): base(authorRepo)
        {
            _mapper = mapper;
            _authorRepository = authorRepo;
        }

        public ResponseDto EditAuthor(AuthorRenameRequest author, string idUser)
        {
            //check author exist
            var authorEdit = _authorRepository.GetById(author.Id);
            if (authorEdit == null)
            {
                return _responseDto.GenerateStrategyResponseDto("userError");
            }
            //update author
            authorEdit.NameAuthor = author.NameAuthor;
            _authorRepository.Update(authorEdit, idUser);
            _authorRepository.SaveChanges();
            return _responseDto.GenerateStrategyResponseDto("success");
        }

        public ResponseDto DeleteAuthor(Guid idEntity, string idUser)
        {
            var oldAuthor = _authorRepository.GetById(idEntity);
            if (oldAuthor == null)
                return _responseDto.GenerateStrategyResponseDto("userError");
            _authorRepository.Delete(idEntity, idUser);
            _authorRepository.SaveChanges();
            return _responseDto.GenerateStrategyResponseDto("success");
        }



        //public async Task<List<Author>> GetAllAuthor()
        //{
        //    List<Author> listAuthor = await context.Authors.ToListAsync();
        //    return listAuthor;
        //}


        //public async Task<int?> PostAuthor( string name )
        //{
        //    try
        //    {
        //        Author newAuthor = new Author() { NameAuthor = name };
        //        await context.Authors.AddAsync(newAuthor);
        //        var test = await context.SaveChangesAsync();

        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Information("Error PostAuthor {t}", ex);
        //        return 1;
        //    }

        //}

        //public async Task<int?> PutAuthor(AuthorRenameRequest author)
        //{
        //    try
        //    {
        //        var oldAuhtor = await context.Authors.FirstOrDefaultAsync(a => a.Id == author.Id);
        //        if (oldAuhtor is null)
        //        {
        //            return 2;
        //        }
        //        oldAuhtor.NameAuthor = author.NameAuthor;
        //        await context.SaveChangesAsync();
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Information("Error PutAuthor {t}", ex);
        //        return 1;
        //    }

        //}


    }
}
