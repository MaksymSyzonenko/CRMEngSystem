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
        } 
    }
}
