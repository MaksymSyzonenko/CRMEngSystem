using CRMEngSystem.Data.Entities.Order;

namespace CRMEngSystem.Data.DefaultData.Order
{
    public static class EquipmentOrderPositionDefaultData
    {
        public static readonly List<EquipmentOrderPositionEntity> EquipmentPositions = new()
        {
            new()
            {
                EquipmentOrderPositionId = 1,
                BasePrice = 10,
                Discount = 35,
                MarkUp = 55,
                Quantity = 3,
                PurchasePrice = 7,
                SellPrice = 12,
                Weight = 15,
                Volume = 0.015M,
                QuantityInStock = 1,
                QuantityToTake = 1,
                ShippingCost = 2,
                EquipmentCatalogPositionId = 1,
                OrderId = 2
            },
            new()
            {
                EquipmentOrderPositionId = 2,
                BasePrice = 15,
                Discount = 35,
                MarkUp = 55,
                Quantity = 2,
                PurchasePrice = 12,
                SellPrice = 21,
                Weight = 24,
                Volume = 0.2M,
                QuantityInStock = 1,
                QuantityToTake = 1,
                ShippingCost = 7,
                EquipmentCatalogPositionId = 2,
                OrderId = 2
            },
            new()
            {
                EquipmentOrderPositionId = 3,
                BasePrice = 100,
                Discount = 35,
                MarkUp = 55,
                Quantity = 1,
                PurchasePrice = 70,
                SellPrice = 120,
                Weight = 76,
                Volume = 3.5M,
                QuantityInStock = 1,
                QuantityToTake = 1,
                ShippingCost = 56,
                EquipmentCatalogPositionId = 3,
                OrderId = 2
            },
            new()
            {
                EquipmentOrderPositionId = 4,
                BasePrice = 496.95M,
                Discount = 44,
                MarkUp = 62,
                Quantity = 6,
                PurchasePrice = 423.65M,
                SellPrice = 789.23M,
                Weight = 3.197M,
                Volume = 0.001833M,
                QuantityInStock = 5,
                QuantityToTake = 3,
                ShippingCost = 45.23M,
                EquipmentCatalogPositionId = 4,
                OrderId = 2
            }
        };
    }
}
