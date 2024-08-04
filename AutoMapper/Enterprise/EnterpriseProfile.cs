using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Dto.Enterprise;
using AutoMapper;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Entities.Order;

namespace CRMEngSystem.AutoMapper.Enterprise
{
    public sealed class EnterpriseProfile : Profile
    {
        public EnterpriseProfile()
        {
            CreateMap<EnterpriseEntity, EnterpriseListItemDto>()
                .ForMember(dest => dest.NameUA, opt => opt.MapFrom(src => src.Details.NameUA))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Details.Country))
                .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Details.Region))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Details.City))
                .ForMember(dest => dest.Coordinate, opt => opt.MapFrom(src => src.Details.Coordinate))
                .ReverseMap();

            CreateMap<EnterpriseEntity, EnterpriseDto>()
                .ForMember(dest => dest.NameUA, opt => opt.MapFrom(src => src.Details.NameUA))
                .ForMember(dest => dest.OwnershipFormUA, opt => opt.MapFrom(src => src.Details.OwnershipFormUA))
                .ForMember(dest => dest.NameEN, opt => opt.MapFrom(src => src.Details.NameEN))
                .ForMember(dest => dest.OwnershipFormEN, opt => opt.MapFrom(src => src.Details.OwnershipFormEN))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Details.Street}, {src.Details.City}, {src.Details.Region}, {src.Details.Country}, {src.Details.PostalCode}"))
                .ForMember(dest => dest.Coordinate, opt => opt.MapFrom(src => src.Details.Coordinate))
                .ForMember(dest => dest.IndustryBranch, opt => opt.MapFrom(src => src.Details.IndustryBranch))
                .ForMember(dest => dest.TradeGroup, opt => opt.MapFrom(src => src.Details.TradeGroup))
                .ForMember(dest => dest.Franchise, opt => opt.MapFrom(src => src.Details.Franchise))
                .ForMember(dest => dest.NumberHighPriorityOrders, opt => opt.MapFrom(src => src.Orders!.Count(order => order.Priority == PriorityValue.High)))
                .ForMember(dest => dest.NumberMediumPriorityOrders, opt => opt.MapFrom(src => src.Orders!.Count(order => order.Priority == PriorityValue.Medium)))
                .ForMember(dest => dest.NumberLowPriorityOrders, opt => opt.MapFrom(src => src.Orders!.Count(order => order.Priority == PriorityValue.Low)))
                .ForMember(dest => dest.TotalOrdersNumber, opt => opt.MapFrom(src => src.Orders!.Count()))
                .ReverseMap();

            CreateMap<OrderEntity, EnterpriseOrderDto>()
                .ForMember(dest => dest.InitiatorInitials, opt => opt.MapFrom(src => $"{src.Initiator.Details.LastName} {src.Initiator.Details.FirstName} {src.Initiator.Details.MiddleName}"))
                .ForMember(dest => dest.TotalSellPrices, opt => opt.MapFrom(src => src.EquipmentOrderPositions!.Sum(position => position.SellPrice * position.Quantity)))
                .ReverseMap();

            CreateMap<EnterpriseEntity, EnterpriseChangeDto>()
                .ForMember(dest => dest.NameUA, opt => opt.MapFrom(src => src.Details.NameUA))
                .ReverseMap();
        }
    }
}
