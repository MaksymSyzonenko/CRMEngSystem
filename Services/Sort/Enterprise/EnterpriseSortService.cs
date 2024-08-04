using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Services.Sort.Core;

namespace CRMEngSystem.Services.Sort.Enterprise
{
    public class EnterpriseSortService : ISortService<EnterpriseEntity>
    {
        private readonly bool? _sortEnterpriseId;
        private readonly bool? _sortAlphabetCountry;
        private readonly bool? _sortAlphabetRegion;
        private readonly bool? _sortAlphabetCity;
        public EnterpriseSortService(bool? sortEnterpriseId, bool? sortAlphabetCountry, bool? sortAlphabetRegion, bool? sortAlphabetCity)
        {
            _sortEnterpriseId = sortEnterpriseId;
            _sortAlphabetCountry = sortAlphabetCountry;
            _sortAlphabetRegion = sortAlphabetRegion;
            _sortAlphabetCity = sortAlphabetCity;
        }

        public IQueryable<EnterpriseEntity> Sort(IQueryable<EnterpriseEntity> entities)
        {
            if(!_sortEnterpriseId.HasValue && !_sortAlphabetCountry.HasValue && !_sortAlphabetRegion.HasValue && !_sortAlphabetCity.HasValue)
                entities = entities.OrderByDescending(entity => entity.EnterpriseId);

            if (_sortEnterpriseId.HasValue)
                entities = _sortEnterpriseId.Value ? entities.OrderByDescending(entity => entity.EnterpriseId) : entities;

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
