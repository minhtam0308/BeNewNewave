using AutoMapper;
using BeNewNewave.DTOs;
using BeNewNewave.Entities;


namespace BeNewNewave.Configs
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //CreateMap<Author, AuthorRenameRequest>()
            //    .ForMember(dest => dest.NameAuthor, opt => opt.MapFrom(src => src.NameAuthor))
            //     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            //CreateMap<AuthorRenameRequest, Author>()
            //    .ForMember(dest => dest.NameAuthor, opt => opt.MapFrom(src => src.NameAuthor))
            //    .ForMember(dest => dest.Id, src => src.Ignore())
            //    .ForMember(dest => dest.RowVersion, src => src.Ignore())
            //    .ForMember(dest => dest.IsDeleted, src => src.Ignore())
            //    .ForMember(dest => dest.CreatedAt, src => src.Ignore());


            CreateMap<Book, BookResponse>()
                .ForMember(bookRes => bookRes.NameAuthor, book => book.MapFrom(src => src.Author.NameAuthor));


            CreateMap<BookRequest, Book>()
                .ForMember(book => book.Title, bookreq => bookreq.MapFrom(src => src.Title))
                .ForMember(book => book.IdAuthor, bookreq => bookreq.MapFrom(src => src.IdAuthor))
                .ForMember(book => book.Description, bookreq => bookreq.MapFrom(src => src.Description))
                .ForMember(book => book.AvailableCopies, bookreq => bookreq.MapFrom(src => src.TotalCopies))
                .ForMember(book => book.TotalCopies, bookreq => bookreq.MapFrom(src => src.TotalCopies))
                .ForMember(book => book.UrlBook, bookreq => bookreq.MapFrom(src => src.UrlBook))
                .ForMember(book => book.Id, bookreq => bookreq.Ignore());

        }
    }
}
