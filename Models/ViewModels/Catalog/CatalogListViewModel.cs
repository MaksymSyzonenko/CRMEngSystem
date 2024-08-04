using CRMEngSystem.Dto.Catalog;
using CRMEngSystem.Models.ViewModels.Pagination;

namespace CRMEngSystem.Models.ViewModels.Catalog
{
    public sealed class CatalogListViewModel : PaginationViewModel
    {
        public int? OrderId { get; init; }
        public int? WareHouseId { get; init; }
        public IEnumerable<CatalogListItemDto> Entities { get; set; } = default!;
        public string? SearchGeneral { get; set; }
        public string? SearchEquipmentCode { get; set; }
        public string? SearchNameEN { get; set; }
        public string? FilterType { get; set; }
        public decimal? FilterMinBasePrice { get; set; }
        public decimal? FilterMaxBasePrice { get; set; }
        public decimal? FilterMinWeight { get; set; }
        public decimal? FilterMaxWeight { get; set; }
        public decimal? FilterMinVolume { get; set; }
        public decimal? FilterMaxVolume { get; set; }
        public bool? SortCode { get; set; }
        public bool? SortAlphabetNameEN { get; set; }
        public bool? SortPrice { get; set; }
        public bool? SortWeight { get; set; }
        public bool? SortVolume { get; set; }
    }
}
