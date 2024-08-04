using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.Core;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Data.Loaders.WareHouse
{
    public sealed class EquipmentWareHouseOrderDataLoader : IEntityDataLoader<EquipmentWareHouseOrderEntity>
    {
        public bool EquipmentOrderPosition { get; init; }
        public bool WareHouse { get; init; }
        public EquipmentWareHouseOrderDataLoader(bool equipmentOrderPosition, bool wareHouse)
        {
            EquipmentOrderPosition = equipmentOrderPosition;
            WareHouse = wareHouse;
        }

        public IQueryable<EquipmentWareHouseOrderEntity> LoadData(IQueryable<EquipmentWareHouseOrderEntity> query)
        {
            query = EquipmentOrderPosition ? query.Include(equipment => equipment.EquipmentOrderPosition) : query;
            query = WareHouse ? query.Include(equipment => equipment.WareHouse) : query;
            return query;
        }
    }
}
