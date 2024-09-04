using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Services.Search.Core;
using System.Text.RegularExpressions;

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
            var result = new List<EquipmentCatalogPositionEntity>();

            foreach (var entity in entities)
            {
                bool matchesSearchGeneral = string.IsNullOrEmpty(_searchGeneral) ||
                    (entity.EquipmentCode != null && entity.EquipmentCode.ToLower() == _searchGeneral.ToLower()) ||
                    (entity.NameEN != null && entity.NameEN.ToLower().Contains(_searchGeneral.ToLower())) ||
                    (entity.NameEN != null && RemoveSpecialCharacters(entity.NameEN.ToLower()).Contains(RemoveSpecialCharacters(_searchGeneral.ToLower()))) ||
                    (entity.NameUA != null && entity.NameUA.ToLower().Contains(_searchGeneral.ToLower())) ||
                    (entity.NameUA != null && RemoveSpecialCharacters(entity.NameUA.ToLower()).Contains(RemoveSpecialCharacters(_searchGeneral.ToLower()))) ||
                    (entity.Producer != null && entity.Producer.ToLower().Contains(_searchGeneral.ToLower())) ||
                    (entity.Country != null && entity.Country.ToLower().Contains(_searchGeneral.ToLower()));

                bool matchesSearchName = string.IsNullOrEmpty(_searchName) ||
                    (entity.NameEN != null && entity.NameEN.ToLower().Contains(_searchName.ToLower())) ||
                    (entity.NameEN != null && RemoveSpecialCharacters(entity.NameEN.ToLower()).Contains(RemoveSpecialCharacters(_searchName.ToLower()))) ||
                    (entity.NameUA != null && entity.NameUA.ToLower().Contains(_searchName.ToLower())) ||
                    (entity.NameUA != null && RemoveSpecialCharacters(entity.NameUA.ToLower()).Contains(RemoveSpecialCharacters(_searchName.ToLower())));

                bool matchesSearchEquipmentCode = string.IsNullOrEmpty(_searchEquipmentCode) ||
                    (entity.EquipmentCode != null && entity.EquipmentCode.ToLower() == _searchEquipmentCode.ToLower());

                if (matchesSearchGeneral && matchesSearchName && matchesSearchEquipmentCode)
                {
                    result.Add(entity);
                }
            }

            return result.AsQueryable();
        }
        private static string RemoveSpecialCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string pattern = @"[\""\*@\/\\'\-\+&\(\)\s]";
            string result = Regex.Replace(input, pattern, string.Empty);
            return result;
        }
    }
}
