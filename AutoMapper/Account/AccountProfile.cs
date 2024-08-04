using AutoMapper;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Dto.Account;

namespace CRMEngSystem.AutoMapper.Account
{
    public sealed class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<UserEntity, AccountDto>().ReverseMap();

            CreateMap<UserEntity, AccountListItemDto>()
                .ForMember(dest => dest.Initials, opt => opt.MapFrom(src => $"{src.Contact.Details.LastName} {src.Contact.Details.FirstName} {src.Contact.Details.MiddleName}"))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
        } 
    }
}
