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
            CreateMap<AuthorRenameRequest, Author>()
                .ForMember(dest => dest.NameAuthor, opt => opt.MapFrom(src => src.NameAuthor))
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.RowVersion, src => src.Ignore())
                .ForMember(dest => dest.IsDeleted, src => src.Ignore())
                .ForMember(dest => dest.CreatedAt, src => src.Ignore());

        }
    }
}
