using AutoMapper;
using CRMEngSystem.Data.Entities.Comment;
using CRMEngSystem.Dto.Comment;

namespace CRMEngSystem.AutoMapper.Comment
{
    public sealed class CommentProfile : Profile
    {
        public CommentProfile() 
        {
            CreateMap<CommentEntity, CommentDto>()
                .ForMember(dest => dest.AuthorInitials, opt => opt.MapFrom(src => $"{src.Author.Details.LastName} {src.Author.Details.FirstName} {src.Author.Details.MiddleName}"))
                .ForMember(dest => dest.AuthorId, opt =>  opt.MapFrom(src => src.AuthorId))
                .ReverseMap();
        }
    }
}
