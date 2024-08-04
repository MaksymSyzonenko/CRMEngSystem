using System.Text.Json;
using CRMEngSystem.Data.Entities.Catalog;

namespace CRMEngSystem.Data.DefaultData.Catalog
{
    public static class EquipmentCatalogPositionDefaultData
    {
        public static List<CatalogPositionImageEntity> CatalogPositionImageDefaultData = new List<CatalogPositionImageEntity>();
        public static List<EquipmentCatalogPositionEntity> GetEquipmentCatalogPositions()
        {
            var list = JsonSerializer.Deserialize<List<EquipmentCatalogPositionEntity>>(File.ReadAllText("EquipmentCatalogPositions.txt"))!;
            int counter = 0;
            foreach (var equipment in list)
            {
                equipment.EquipmentCatalogPositionId = ++counter;
                var image = new CatalogPositionImageEntity
                {
                    CatalogPositionImageId = ++counter,
                    Bytes = null,
                    EquipmentCatalogPositionId = equipment.EquipmentCatalogPositionId
                };
                CatalogPositionImageDefaultData.Add(image);
            }
            return list;
        }
        public static readonly List<EquipmentCatalogPositionEntity> EquipmentCatalogPositions = GetEquipmentCatalogPositions();
    }
}
