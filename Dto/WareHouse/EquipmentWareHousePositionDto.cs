namespace CRMEngSystem.Dto.WareHouse
{
    public sealed class EquipmentWareHousePositionDto
    {
        public int EquipmentCatalogPositionId { get; set; }
        public int EquipmentWareHousePositionId { get; set; }
        public string EquipmentCode { get; set; } = default!;
        public string NameUA { get; set; } = default!;
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public decimal BasePrice { get; set; }
        public int Quantity { get; set; }
        public int Number { get; set; }
    }
}
