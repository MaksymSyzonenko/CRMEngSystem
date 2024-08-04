using System.ComponentModel.DataAnnotations;

namespace CRMEngSystem.Models.ViewModels.Catalog
{
    public sealed class CatalogEditViewModel
    {
        public int EquipmentCatalogPositionId { get; init; } = default!;
        public string EquipmentCode { get; init; } = default!;
        public IFormFile Image { get; init; } = default!;
        public string NameUA { get; init; } = default!;
        public string NameEN { get; init; } = default!;
        public string Type { get; init; } = default!;
        public string? Producer { get; init; }
        public string? Country { get; init; }
        public decimal BasePrice { get; init; }
        public decimal Weight { get; init; }
        public decimal Volume { get; init; }
        public string? Link { get; init; }
    }
}
