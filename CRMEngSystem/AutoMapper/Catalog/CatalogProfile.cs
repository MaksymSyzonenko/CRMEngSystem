using AutoMapper;
using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Dto.Catalog;

namespace CRMEngSystem.AutoMapper.Catalog
{
    public sealed class CatalogProfile : Profile
    {
        public CatalogProfile() 
        {
            CreateMap<EquipmentCatalogPositionEntity, CatalogListItemDto>().ReverseMap();

            CreateMap<EquipmentCatalogPositionEntity, CatalogPositionDto>()
               .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image!.Bytes))
               .ReverseMap();

        }
    }
}
