using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Data.Loaders.Core;

namespace CRMEngSystem.Data.Loaders.Catalog
{
    public sealed class EquipmentCatalogPositionDataLoader : IEntityDataLoader<EquipmentCatalogPositionEntity>
    {
        public bool Image { get; init; }
        public bool EquipmentOrderPosition { get; init; }
        public bool EquipmentWareHousePosition { get; init; }
        public EquipmentCatalogPositionDataLoader(bool image, bool equipmentOrderPosition, bool equipmentWareHousePosition)
        {
            Image = image;
            EquipmentOrderPosition = equipmentOrderPosition;
            EquipmentWareHousePosition = equipmentWareHousePosition;
        }

        public IQueryable<EquipmentCatalogPositionEntity> LoadData(IQueryable<EquipmentCatalogPositionEntity> query)
        {
            query = Image ? query.Include(equipment => equipment.Image) : query;          
            query = EquipmentOrderPosition ? query.Include(equipment => equipment.EquipmentOrderPositions) : query;
            query = EquipmentWareHousePosition ? query.Include(equipment => equipment.EquipmentWareHousePositions) : query;
            return query;
        }
    }
}
