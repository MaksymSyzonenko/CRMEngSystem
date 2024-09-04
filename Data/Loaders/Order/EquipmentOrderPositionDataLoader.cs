using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Loaders.Core;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Data.Loaders.Order
{
    public sealed class EquipmentOrderPositionDataLoader : IEntityDataLoader<EquipmentOrderPositionEntity>
    {
        public bool Order { get; init; }
        public bool EquipmentCatalogPosition { get; init; }
        public EquipmentOrderPositionDataLoader(bool order, bool equipmentCatalogPosition)
        {
            Order = order;
            EquipmentCatalogPosition = equipmentCatalogPosition;
        }
        public IQueryable<EquipmentOrderPositionEntity> LoadData(IQueryable<EquipmentOrderPositionEntity> query)
        {
            query = Order ? query.Include(equipment => equipment.Order) : query;
            query = EquipmentCatalogPosition ? query.Include(equipment => equipment.EquipmentCatalogPosition).ThenInclude(equipment => equipment.Image) : query;
            return query;
        }
    }
}
