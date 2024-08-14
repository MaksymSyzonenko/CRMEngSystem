using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Services.Search.Core;

namespace CRMEngSystem.Services.Search.Catalog
{
    public sealed class CatalogSearchService : ISearchService<EquipmentCatalogPositionEntity>
    {
        private readonly string? _searchGeneral;
        private readonly string? _searchEquipmentCode;
        private readonly string? _searchName;
        public CatalogSearchService(string? searchGeneral, string? searchEquipmentCode, string? searchName)
        {
            _searchGeneral = searchGeneral;
            _searchEquipmentCode = searchEquipmentCode;
            _searchName = searchName;
        }

        public IQueryable<EquipmentCatalogPositionEntity> Search(IQueryable<EquipmentCatalogPositionEntity> entities)
        {
            entities = string.IsNullOrEmpty(_searchGeneral) ? entities : entities.Where(entity =>
                (entity.EquipmentCode != null && entity.EquipmentCode.ToLower() == _searchGeneral.ToLower()) ||
                (entity.NameEN != null && entity.NameEN.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.NameUA != null && entity.NameUA.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Producer != null && entity.Producer.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Country != null && entity.Country.ToLower().Contains(_searchGeneral.ToLower())));

            entities = entities.Where(entity =>
                (string.IsNullOrEmpty(_searchName) ||
                    (entity.NameEN != null && entity.NameEN.ToLower().Contains(_searchName.ToLower())) ||
                    (entity.NameUA != null && entity.NameUA.ToLower().Contains(_searchName.ToLower()))) &&
                (string.IsNullOrEmpty(_searchEquipmentCode) ||
                    (entity.EquipmentCode != null && entity.EquipmentCode.ToLower() == _searchEquipmentCode.ToLower())));

            return entities;
        }
    }
}
