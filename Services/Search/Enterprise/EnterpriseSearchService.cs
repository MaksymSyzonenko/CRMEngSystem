using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Services.Search.Core;

namespace CRMEngSystem.Services.Search.Enterprise
{
    public sealed class EnterpriseSearchService : ISearchService<EnterpriseEntity>
    {
        private readonly string? _searchGeneral;
        private readonly string? _searchName;
        private readonly string? _searchStreet;
        private readonly string? _searchRegion;
        private readonly string? _searchCity;
        public EnterpriseSearchService(string? searchGeneral, string? searchName, string? searchStreet, string? searchRegion, string? searchCity)
        {
            _searchGeneral = searchGeneral;
            _searchName = searchName;
            _searchStreet = searchStreet;
            _searchRegion = searchRegion;
            _searchCity = searchCity;
        }

        public IQueryable<EnterpriseEntity> Search(IQueryable<EnterpriseEntity> entities)
        {
            entities = string.IsNullOrEmpty(_searchGeneral) ? entities : entities.Where(entity =>
                (entity.Details.NameUA != null && entity.Details.NameUA.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.NameEN != null && entity.Details.NameEN.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.OwnershipFormUA != null && entity.Details.OwnershipFormUA.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.OwnershipFormEN != null && entity.Details.OwnershipFormEN.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.IndustryBranch != null && entity.Details.IndustryBranch.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.Franchise != null && entity.Details.Franchise.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.Country != null && entity.Details.Country.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.Region != null && entity.Details.Region.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.City != null && entity.Details.City.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.PostalCode != null && entity.Details.PostalCode.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.Street != null && entity.Details.Street.ToLower().Contains(_searchGeneral.ToLower())));

            entities = entities.Where(entity =>
                (string.IsNullOrEmpty(_searchName) ||
                    (entity.Details.NameUA != null && entity.Details.NameUA.ToLower().Contains(_searchName.ToLower())) ||
                    (entity.Details.NameEN != null && entity.Details.NameEN.ToLower().Contains(_searchName.ToLower()))) &&
                (string.IsNullOrEmpty(_searchStreet) ||
                    (entity.Details.Street != null && entity.Details.Street.ToLower().Contains(_searchStreet.ToLower()))) &&
                (string.IsNullOrEmpty(_searchRegion) ||
                    (entity.Details.Region != null && entity.Details.Region.ToLower().Contains(_searchRegion.ToLower()))) &&
                (string.IsNullOrEmpty(_searchCity) ||
                    (entity.Details.City != null && entity.Details.City.ToLower().Contains(_searchCity.ToLower()))));

            return entities;
        }
    }
}
