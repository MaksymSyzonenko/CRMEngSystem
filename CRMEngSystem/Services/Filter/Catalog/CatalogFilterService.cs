using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Services.Filter.Core;

namespace CRMEngSystem.Services.Filter.Catalog
{
    public class CatalogFilterService : IFilterService<EquipmentCatalogPositionEntity>
    {
        private readonly string? _filterType;
        private readonly decimal? _filterMinBasePrice;
        private readonly decimal? _filterMaxBasePrice;
        private readonly decimal? _filterMinWeight;
        private readonly decimal? _filterMaxWeight;
        private readonly decimal? _filterMinVolume;
        private readonly decimal? _filterMaxVolume;
        public CatalogFilterService(string? filterType, decimal? filterMinBasePrice, decimal? filterMaxBasePrice, decimal? filterMinWeight, decimal? filterMaxWeight, decimal? filterMinVolume, decimal? filterMaxVolume)
        {
            _filterType = filterType;
            _filterMinBasePrice = filterMinBasePrice;
            _filterMaxBasePrice = filterMaxBasePrice;
            _filterMinWeight = filterMinWeight;
            _filterMaxWeight = filterMaxWeight;
            _filterMinVolume = filterMinVolume;
            _filterMaxVolume = filterMaxVolume;
        }

        public IQueryable<EquipmentCatalogPositionEntity> Filter(IQueryable<EquipmentCatalogPositionEntity> entities)
        {
            if (!string.IsNullOrEmpty(_filterType) && CatalogFilterMap.TypeMap.TryGetValue(_filterType, out var type))
                entities = entities.Where(entity => entity.Type == type);

            if(_filterMinBasePrice != null)
                entities = entities.Where(entity => entity.BasePrice >= _filterMinBasePrice);

            if (_filterMaxBasePrice != null)
                entities = entities.Where(entity => entity.BasePrice <= _filterMaxBasePrice);

            if (_filterMinWeight != null)
                entities = entities.Where(entity => entity.Weight >= _filterMinWeight);

            if (_filterMaxWeight != null)
                entities = entities.Where(entity => entity.Weight <= _filterMaxWeight);

            if (_filterMinVolume != null)
                entities = entities.Where(entity => entity.Volume >= _filterMinVolume);

            if (_filterMaxVolume != null)
                entities = entities.Where(entity => entity.Volume <= _filterMaxVolume);

            return entities;
        }
    }
}
