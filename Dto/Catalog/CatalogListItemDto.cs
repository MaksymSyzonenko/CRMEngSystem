namespace CRMEngSystem.Dto.Catalog
{
    public sealed record class CatalogListItemDto
    {
        public int EquipmentCatalogPositionId { get; init; }
        public string EquipmentCode { get; init; } = default!;
        public string NameEN { get; init; } = default!;
        public string Type { get; init; } = default!;
        public decimal BasePrice { get; init; }
        public decimal Weight { get; init; }
        public decimal Volume { get; init; }
        public int Quantity { get; set; }
        public DateTime DateTimeUpdatePrice { get; init; }
    }
}
