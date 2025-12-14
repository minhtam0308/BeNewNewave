

using AutoMapper;
using Azure;
using BeNewNewave.Data;
using BeNewNewave.DTOs;
using BeNewNewave.Entities;
using BeNewNewave.Interface.IRepositories;
using BeNewNewave.Interface.Service;
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

        public AuthorServices(IAuthorRepository authorRepo, IMapper mapper): base(authorRepo)
        {
            _mapper = mapper;
        }

        public ResponseDto EditAuthor(AuthorRenameRequest author, Guid id)
        {
            var authorEdit = _repo.GetById(author.Id);
            if (authorEdit == null)
            {
                _responseDto.SetResponseDtoStrategy(new UserError());
                return _responseDto.GetResponseDto();
            }
            _repo.Update(authorEdit);
            _repo.SaveChanges();
            _responseDto.SetResponseDtoStrategy(new Success());
            return _responseDto.GetResponseDto();
        }

        public ResponseDto DeleteAuthor(Guid idEntity, string idUser)
        {
            var oldAuthor = _repo.GetById(idEntity);
            if (oldAuthor == null)
                return GenerateStrategyResponseDto("userError");
            _repo.Delete(idEntity, idUser);
            _repo.SaveChanges();
            return GenerateStrategyResponseDto("success");
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

        private ResponseDto GenerateStrategyResponseDto(string result)
        {
            switch (result)
            {
                case "userError":
                    _responseDto.SetResponseDtoStrategy(new UserError());
                    return _responseDto.GetResponseDto();
                case "serverError":
                    _responseDto.SetResponseDtoStrategy(new ServerError());
                    return _responseDto.GetResponseDto();
                default:
                    _responseDto.SetResponseDtoStrategy(new Success());
                    return _responseDto.GetResponseDto();
            }
        }

    }
}
