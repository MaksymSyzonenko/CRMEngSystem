using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Dto.Order;
using AutoMapper;

namespace CRMEngSystem.AutoMapper.Order
{
    public sealed class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderEntity, OrderListItemDto>()
                .ForMember(dest => dest.CustomerNameUA, opt => opt.MapFrom(src => src.Customer.Details.NameUA))
                .ForMember(dest => dest.InitiatorInitials, opt => opt.MapFrom(src => $"{src.Initiator.Details.LastName} {src.Initiator.Details.FirstName} {src.Initiator.Details.MiddleName}"))
                .ReverseMap();

            CreateMap<OrderEntity, OrderDto>()
                .ForMember(dest => dest.CustomerNameUA, opt => opt.MapFrom(src => src.Customer.Details.NameUA))
                .ForMember(dest => dest.InitiatorInitials, opt => opt.MapFrom(src => $"{src.Initiator.Details.LastName} {src.Initiator.Details.FirstName} {src.Initiator.Details.MiddleName}"))
                .ForMember(dest => dest.CustomerAddress, opt => opt.MapFrom(src => $"{src.Customer.Details.Street}, {src.Customer.Details.City}, {src.Customer.Details.Region}, {src.Customer.Details.Country}, {src.Customer.Details.PostalCode}"))
                .ForMember(dest => dest.NumberEquipmentPositions, opt => opt.MapFrom(src => src.EquipmentOrderPositions!.Sum(position => position.Quantity)))
                .ForMember(dest => dest.TotalWeight, opt => opt.MapFrom(src => src.EquipmentOrderPositions!.Sum(position => position.Weight * position.Quantity)))
                .ForMember(dest => dest.TotalVolume, opt => opt.MapFrom(src => src.EquipmentOrderPositions!.Sum(position => position.Volume * position.Quantity)))
                .ForMember(dest => dest.TotalBasePrices, opt => opt.MapFrom(src => src.EquipmentOrderPositions!.Sum(position => position.BasePrice * position.Quantity)))
                .ForMember(dest => dest.TotalPurchasePrices, opt => opt.MapFrom(src => src.EquipmentOrderPositions!.Sum(position => position.PurchasePrice * position.Quantity)))
                .ForMember(dest => dest.TotalSellPrices, opt => opt.MapFrom(src => src.EquipmentOrderPositions!.Sum(position => position.SellPrice * position.Quantity)))
                .ForMember(dest => dest.TotalShippingCosts, opt => opt.MapFrom(src => src.EquipmentOrderPositions!.Sum(position => position.ShippingCost * position.Quantity)))
                .ReverseMap();

            CreateMap<EquipmentOrderPositionEntity, OrderEquipmentPositionDto>()
                .ForMember(dest => dest.NameUA, opt => opt.MapFrom(src => src.EquipmentCatalogPosition.NameUA))
                .ForMember(dest => dest.EquipmentCode, opt => opt.MapFrom(src => src.EquipmentCatalogPosition.EquipmentCode))
                .ForMember(dest => dest.EquipmentCatalogPositionId, opt => opt.MapFrom(src => src.EquipmentCatalogPosition.EquipmentCatalogPositionId))
                .ReverseMap();

            CreateMap<EquipmentOrderPositionEntity, OrderEquipmentPositionDetailsDto>()
                .ForMember(dest => dest.NameUA, opt => opt.MapFrom(src => src.EquipmentCatalogPosition.NameUA))
                .ForMember(dest => dest.EquipmentCode, opt => opt.MapFrom(src => src.EquipmentCatalogPosition.EquipmentCode))
                .ForMember(dest => dest.EquipmentCatalogPositionId, opt => opt.MapFrom(src => src.EquipmentCatalogPosition.EquipmentCatalogPositionId))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.EquipmentCatalogPosition.Image!.Bytes))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.EquipmentCatalogPosition.Type.ToString()))
                .ReverseMap();

            CreateMap<OrderEntity, OrdersCreateOrderDto>()
                .ForMember(dest => dest.CustomerNameUA, opt => opt.MapFrom(src => src.Customer.Details.NameUA)) 
                .ReverseMap();
        }
    }
}
