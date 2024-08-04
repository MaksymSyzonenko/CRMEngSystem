using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Dto.Contact;
using AutoMapper;

namespace CRMEngSystem.AutoMapper.Contact
{
    public sealed class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<ContactEntity, ContactListItemDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image!.Bytes))
                .ForMember(dest => dest.Initials, opt => opt.MapFrom(src => $"{src.Details.LastName} {src.Details.FirstName} {src.Details.MiddleName}"))
                .ForMember(dest => dest.EnterpriseNameUA, opt => opt.MapFrom(src => src.Enterprise.Details.NameUA))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Details.FirstPhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Details.FirstEmail))
                .ReverseMap();

            CreateMap<ContactEntity, ContactDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image!.Bytes))
                .ForMember(dest => dest.Initials, opt => opt.MapFrom(src => $"{src.Details.LastName} {src.Details.FirstName} {src.Details.MiddleName}"))
                .ForMember(dest => dest.EnterpriseNameUA, opt => opt.MapFrom(src => src.Enterprise.Details.NameUA))
                .ForMember(dest => dest.EnterpriseAddress, opt => opt.MapFrom(src => $"{src.Enterprise.Details.Street}, {src.Enterprise.Details.City}, {src.Enterprise.Details.Region}, {src.Enterprise.Details.Country}, {src.Enterprise.Details.PostalCode}"))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Details.Position))
                .ForMember(dest => dest.FirstPhoneNumber, opt => opt.MapFrom(src => src.Details.FirstPhoneNumber))
                .ForMember(dest => dest.ExtraPhoneNumber, opt => opt.MapFrom(src => src.Details.ExtraPhoneNumber))
                .ForMember(dest => dest.FirstEmail, opt => opt.MapFrom(src => src.Details.FirstEmail))
                .ForMember(dest => dest.ExtraEmail, opt => opt.MapFrom(src => src.Details.ExtraEmail))
                .ForMember(dest => dest.NumberHighPriorityOrders, opt => opt.MapFrom(src => src.ContactOrders!.Select(contactorder => contactorder.Order).Count(order => order.Priority == PriorityValue.High)))
                .ForMember(dest => dest.NumberMediumPriorityOrders, opt => opt.MapFrom(src => src.ContactOrders!.Select(contactorder => contactorder.Order).Count(order => order.Priority == PriorityValue.Medium)))
                .ForMember(dest => dest.NumberLowPriorityOrders, opt => opt.MapFrom(src => src.ContactOrders!.Select(contactorder => contactorder.Order).Count(order => order.Priority == PriorityValue.Low)))
                .ForMember(dest => dest.TotalOrdersNumber, opt => opt.MapFrom(src => src.ContactOrders!.Select(contactorder => contactorder.Order).Count()))
                .ForMember(dest => dest.TelegramLink, opt => opt.MapFrom(src => src.Details!.TelegramLink))
                .ForMember(dest => dest.LinkedInLink, opt => opt.MapFrom(src => src.Details!.LinkedInLink))
                .ForMember(dest => dest.ViberLink, opt => opt.MapFrom(src => src.Details!.ViberLink))
                .ForMember(dest => dest.FaceBookLink, opt => opt.MapFrom(src => src.Details!.FaceBookLink))
                .ForMember(dest => dest.WhatsappLink, opt => opt.MapFrom(src => src.Details!.WhatsappLink))
                .ForMember(dest => dest.SkypeLink, opt => opt.MapFrom(src => src.Details!.SkypeLink))
                .ForMember(dest => dest.TwitterLink, opt => opt.MapFrom(src => src.Details!.TwitterLink))
                .ForMember(dest => dest.InstagramLink, opt => opt.MapFrom(src => src.Details!.InstagramLink))
                .ReverseMap();

            CreateMap<ContactEntity, ContactSelectDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image!.Bytes))
                .ForMember(dest => dest.Initials, opt => opt.MapFrom(src => $"{src.Details.LastName} {src.Details.FirstName} {src.Details.MiddleName}"))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Details.FirstPhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Details.FirstEmail))
                .ReverseMap();

            CreateMap<ContactEntity, ContactOrderDto>()
               .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image!.Bytes))
               .ForMember(dest => dest.Initials, opt => opt.MapFrom(src => $"{src.Details.LastName} {src.Details.FirstName} {src.Details.MiddleName}"))
               .ForMember(dest => dest.EnterpriseNameUA, opt => opt.MapFrom(src => src.Enterprise.Details.NameUA))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Details.FirstPhoneNumber))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Details.FirstEmail))
               .ReverseMap();
        }
    }
}
