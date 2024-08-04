using CRMEngSystem.Data.Enums;

namespace CRMEngSystem.Dto.Catalog
{
    public sealed class CatalogPositionDto
    {
        public int EquipmentCatalogPositionId { get; set; }
        public string EquipmentCode { get; init; } = default!;
        public byte[]? Image { get; init; }
        public string NameUA { get; set; } = default!;
        public string NameEN { get; set; } = default!;
        public EquipmentTypeValue Type { get; set; }
        public string? Producer { get; set; }
        public string? Country { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime DateTimeCreate { get; set; }
        public DateTime? DateTimeUpdate { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public string? Link { get; set; }
    }
}
