using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.Core;

namespace CRMEngSystem.Data.Entities.WareHouse
{
    public class EquipmentWareHouseOrderEntity : IEntity
    {
        public int EquipmentWareHouseOrderId { get; set; }

        //Data Properties
        public int Quantity { get; set; }

        //Navigation Properties
        public int EquipmentOrderPositionId { get; set; } = default!;
        public virtual EquipmentOrderPositionEntity EquipmentOrderPosition { get; set; } = null!;
        public int WareHouseId { get; set; }
        public virtual WareHouseEntity WareHouse { get; set; } = null!;
    }
}
