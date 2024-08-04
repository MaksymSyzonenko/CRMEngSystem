using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Data.Entities.Core;
using CRMEngSystem.Data.Entities.WareHouse;

namespace CRMEngSystem.Data.Entities.Order
{
    public class EquipmentOrderPositionEntity : IEntity
    {
        public int EquipmentOrderPositionId { get; set; }

        //Data Properties
        public decimal BasePrice { get; set; }
        public int Discount { get; set; }
        public int MarkUp { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public int QuantityInStock { get; set; }
        public int QuantityToTake { get; set; }
        public decimal ShippingCost { get; set; }

        //Navigation Properties
        public int EquipmentCatalogPositionId { get; set; } = default!;
        public virtual EquipmentCatalogPositionEntity EquipmentCatalogPosition { get; set; } = null!;
        public int OrderId { get; set; }
        public virtual OrderEntity Order { get; set; } = null!;
        public ICollection<EquipmentWareHouseOrderEntity>? EquipmentWareHouseOrder { get; set; }
    }
}
