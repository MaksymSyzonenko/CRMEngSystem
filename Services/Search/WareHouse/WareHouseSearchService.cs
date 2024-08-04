using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Services.Search.Core;

namespace CRMEngSystem.Services.Search.WareHouse
{
    public sealed class WareHouseSearchService : ISearchService<WareHouseEntity>
    {
        private readonly string? _searchGeneral;
        public WareHouseSearchService(string? searchGeneral)
        {
            _searchGeneral = searchGeneral;
        }
        public IQueryable<WareHouseEntity> Search(IQueryable<WareHouseEntity> entities)
        {
            entities = string.IsNullOrEmpty(_searchGeneral) ? entities : entities.Where(entity =>
                (entity.Name != null && entity.Name.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Country != null && entity.Country.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Region != null && entity.Region.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.City != null && entity.City.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.PostalCode != null && entity.PostalCode.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Street != null && entity.Street.ToLower().Contains(_searchGeneral.ToLower())));

            return entities;
        }
    }
}
