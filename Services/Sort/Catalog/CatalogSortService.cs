using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Services.Sort.Core;

namespace CRMEngSystem.Services.Sort.Catalog
{
    public class CatalogSortService : ISortService<EquipmentCatalogPositionEntity>
    {
        private readonly bool? _sortCode;
        private readonly bool? _sortAlphabetNameEN;
        private readonly bool? _sortPrice;
        private readonly bool? _sortWeight;
        private readonly bool? _sortVolume;
        public CatalogSortService(bool? sortCode, bool? sortAlphabetNameEN, bool? sortPrice, bool? sortWeight, bool? sortVolume)
        {
            _sortCode = sortCode;
            _sortAlphabetNameEN = sortAlphabetNameEN;
            _sortPrice = sortPrice;
            _sortWeight = sortWeight;
            _sortVolume = sortVolume;
        }

        public IQueryable<EquipmentCatalogPositionEntity> Sort(IQueryable<EquipmentCatalogPositionEntity> entities)
        {
            if(!_sortCode.HasValue && !_sortAlphabetNameEN.HasValue && !_sortPrice.HasValue && !_sortWeight.HasValue && !_sortVolume.HasValue)
                entities = entities.OrderBy(entity => entity.EquipmentCode);

            if (_sortCode.HasValue)
                entities = _sortCode.Value ? entities.OrderBy(entity => entity.EquipmentCode) : entities;

            if (_sortAlphabetNameEN.HasValue)
                entities = _sortAlphabetNameEN.Value ? entities.OrderBy(entity => entity.NameEN) : entities;

            if (_sortPrice.HasValue)
                entities = _sortPrice.Value ? entities.OrderByDescending(entity => entity.BasePrice) : entities;

            if (_sortWeight.HasValue)
                entities = _sortWeight.Value ? entities.OrderByDescending(entity => entity.Weight) : entities;

            if (_sortVolume.HasValue)
                entities = _sortVolume.Value ? entities.OrderByDescending(entity => entity.Volume) : entities;

            return entities;
        }
    }
}
