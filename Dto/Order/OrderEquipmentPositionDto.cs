namespace CRMEngSystem.Dto.Order
{
    public sealed record class OrderEquipmentPositionDto
    {
        public int OrderId { get; init; }
        public int EquipmentCatalogPositionId { get; init; }
        public int EquipmentOrderPositionId { get; init; }
        public string EquipmentCode { get; init; } = default!;
        public string NameUA { get; init; } = default!;
        public decimal BasePrice { get; init; }
        public int Discount { get; init; }
        public int MarkUp { get; init; }
        public decimal SellPrice { get; init; }
        public int Quantity { get; init; }
        public int QuantityInStock { get; set; }
        public int QuantityToTake { get; set; }
        public decimal ShippingCost { get; init; }
    }
}
