using CRMEngSystem.Dto.Catalog;
using CRMEngSystem.Models.ViewModels.Pagination;

namespace CRMEngSystem.Models.ViewModels.Catalog
{
    public sealed class CatalogListViewModel : PaginationViewModel
    {
        public int? OrderId { get; init; }
        public IEnumerable<CatalogListItemDto> Entities { get; set; } = default!;
        public string? SearchGeneral { get; init; }
        public string? SearchEquipmentCode { get; init; }
        public string? SearchNameEN { get; init; }
        public string? FilterType { get; init; }
        public decimal? FilterMinBasePrice { get; init; }
        public decimal? FilterMaxBasePrice { get; init; }
        public decimal? FilterMinWeight { get; init; }
        public decimal? FilterMaxWeight { get; init; }
        public decimal? FilterMinVolume { get; init; }
        public decimal? FilterMaxVolume { get; init; }
        public bool? SortCode { get; init; }
        public bool? SortAlphabetNameEN { get; init; }
        public bool? SortPrice { get; init; }
        public bool? SortWeight { get; init; }
        public bool? SortVolume { get; init; }
    }
}
