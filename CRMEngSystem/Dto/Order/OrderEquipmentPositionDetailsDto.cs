namespace CRMEngSystem.Dto.Order
{
    public sealed class OrderEquipmentPositionDetailsDto
    {
        public int OrderId { get; init; }
        public byte[]? Image { get; init; }
        public int EquipmentCatalogPositionId { get; init; }
        public int EquipmentOrderPositionId { get; init; }
        public string NameUA { get; init; } = default!;
        public string EquipmentCode { get; init; } = default!;
        public decimal BasePrice { get; init; }
        public decimal SellPrice { get; init; }
        public decimal PurchasePrice { get; init; }
        public int Quantity { get; init; }
        public string Type { get; init; } = default!;
        public decimal ShippingCost { get; init; }
        public int Discount { get; init; }
        public int MarkUp { get; init; }
        public decimal Weight { get; init; }
        public decimal Volume { get; init; }
    }
}
