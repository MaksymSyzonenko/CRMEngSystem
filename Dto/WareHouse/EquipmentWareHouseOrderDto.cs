
namespace CRMEngSystem.Dto.WareHouse
{
    public sealed class EquipmentWareHouseOrderDto
    {
        public int Quantity { get; set; }
        public int QuantityInStock { get; set; }
        public int EquipmentOrderPositionId { get; set; }
        public int EquipmentCatalogPositionId { get; set; }
        public int WareHouseId { get; set; }
        public string WareHouseName { get; set; } = default!;
    }
}
