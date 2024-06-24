using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Services.Sort.Core;

namespace CRMEngSystem.Services.Sort.Enterprise
{
    public class EnterpriseSortService : ISortService<EnterpriseEntity>
    {
        private readonly bool? _sortAlphabetNameUA;
        private readonly bool? _sortAlphabetCountry;
        private readonly bool? _sortAlphabetRegion;
        private readonly bool? _sortAlphabetCity;
        public EnterpriseSortService(bool? sortAlphabetNameUA, bool? sortAlphabetCountry, bool? sortAlphabetRegion, bool? sortAlphabetCity)
        {
            _sortAlphabetNameUA = sortAlphabetNameUA;
            _sortAlphabetCountry = sortAlphabetCountry;
            _sortAlphabetRegion = sortAlphabetRegion;
            _sortAlphabetCity = sortAlphabetCity;
        }

        public IQueryable<EnterpriseEntity> Sort(IQueryable<EnterpriseEntity> entities)
        {
            if(!_sortAlphabetNameUA.HasValue && !_sortAlphabetCountry.HasValue && !_sortAlphabetRegion.HasValue && !_sortAlphabetCity.HasValue)
                entities = entities.OrderBy(entity => entity.Details.NameUA);

            if (_sortAlphabetNameUA.HasValue)
                entities = _sortAlphabetNameUA.Value ? entities.OrderBy(entity => entity.Details.NameUA) : entities;

            if (_sortAlphabetCountry.HasValue)
                entities = _sortAlphabetCountry.Value ? entities.OrderBy(entity => entity.Details.Country) : entities;

            if (_sortAlphabetRegion.HasValue)
                entities = _sortAlphabetRegion.Value ? entities.OrderBy(entity => entity.Details.Region) : entities;

            if (_sortAlphabetCity.HasValue)
                entities = _sortAlphabetCity.Value ? entities.OrderBy(entity => entity.Details.City) : entities;

            return entities;
        }
    }
}
