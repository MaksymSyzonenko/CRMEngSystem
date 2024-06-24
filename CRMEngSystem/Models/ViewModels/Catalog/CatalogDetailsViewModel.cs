using CRMEngSystem.Dto.Catalog;

namespace CRMEngSystem.Models.ViewModels.Catalog
{
    public sealed class CatalogDetailsViewModel
    {
        public int EntityId { get; init; } = default!;
        public CatalogPositionDto CatalogPosition { get; init; } = default!;
    }
}
