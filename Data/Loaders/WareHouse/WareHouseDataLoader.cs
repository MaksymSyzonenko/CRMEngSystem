using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.Core;

namespace CRMEngSystem.Data.Loaders.WareHouse
{
    public sealed class WareHouseDataLoader : IEntityDataLoader<WareHouseEntity>
    {
        public bool EquipmentWareHousePositions { get; init; }
        public WareHouseDataLoader(bool equipmentWareHousePositions)
        {
            EquipmentWareHousePositions = equipmentWareHousePositions;
        }

        public IQueryable<WareHouseEntity> LoadData(IQueryable<WareHouseEntity> query)
        {
            query = EquipmentWareHousePositions ? query.Include(warehouse => warehouse.EquipmentWareHousePositions).ThenInclude(equipment => equipment.EquipmentCatalogPosition) : query;
            return query;
        }
    }
}
