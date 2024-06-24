using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.Core;

namespace CRMEngSystem.Data.Loaders.WareHouse
{
    public sealed class EquipmentWareHousePositionDataLoader : IEntityDataLoader<EquipmentWareHousePositionEntity>
    {
        public bool EquipmentCatalogPosition { get; init; }
        public bool WareHouse { get; init; }
        public EquipmentWareHousePositionDataLoader(bool equipmentCatalogPosition, bool wareHouse)
        {
            EquipmentCatalogPosition = equipmentCatalogPosition;
            WareHouse = wareHouse;
        }

        public IQueryable<EquipmentWareHousePositionEntity> LoadData(IQueryable<EquipmentWareHousePositionEntity> query)
        {
            query = EquipmentCatalogPosition ? query.Include(equipment => equipment.EquipmentCatalogPosition) : query;
            query = WareHouse ? query.Include(equipment => equipment.WareHouse) : query;
            return query;
        }
    }
}
