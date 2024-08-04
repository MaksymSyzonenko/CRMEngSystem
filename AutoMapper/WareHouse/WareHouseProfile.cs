using AutoMapper;
using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Dto.WareHouse;

namespace CRMEngSystem.AutoMapper.WareHouse
{
    public sealed class WareHouseProfile : Profile
    {
        public WareHouseProfile()
        {
            CreateMap<WareHouseEntity, WareHouseListItemDto>().ReverseMap();

            CreateMap<EquipmentWareHousePositionEntity, EquipmentWareHousePositionDto>()
                .ForMember(dest => dest.NameUA, opt => opt.MapFrom(src => src.EquipmentCatalogPosition.NameUA))
                .ForMember(dest => dest.EquipmentCode, opt => opt.MapFrom(src => src.EquipmentCatalogPosition.EquipmentCode))
                .ForMember(dest => dest.BasePrice, opt => opt.MapFrom(src => src.EquipmentCatalogPosition.BasePrice))
                .ForMember(dest => dest.Volume, opt => opt.MapFrom(src => src.EquipmentCatalogPosition.Volume))
                .ForMember(dest => dest.Weight, opt => opt.MapFrom(src => src.EquipmentCatalogPosition.Weight))
                .ReverseMap();
        }
    }
}
