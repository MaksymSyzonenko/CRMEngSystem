using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Data.Entities.Core;

namespace CRMEngSystem.Data.Entities.WareHouse
{
    public class EquipmentWareHousePositionEntity : IEntity
    {
        public int EquipmentWareHousePositionId { get; set; }

        //Data Properties
        public int Quantity { get; set; }

        //Navigation Properties
        public int EquipmentCatalogPositionId { get; set; } = default!;
        public virtual EquipmentCatalogPositionEntity EquipmentCatalogPosition { get; set; } = null!;
        public int WareHouseId { get; set; }
        public virtual WareHouseEntity WareHouse { get; set; } = null!;
    }
}
