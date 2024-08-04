using CRMEngSystem.Dto.WareHouse;
using CRMEngSystem.Models.ViewModels.Pagination;

namespace CRMEngSystem.Models.ViewModels.WareHouse
{
    public sealed class WareHouseListViewModel : PaginationViewModel
    {
        public IEnumerable<WareHouseListItemDto> Entities { get; set; } = default!;
        public string? SearchGeneral { get; init; }
    }
}
