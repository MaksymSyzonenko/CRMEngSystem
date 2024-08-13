using CRMEngSystem.Dto.Enterprise;
using CRMEngSystem.Models.ViewModels.Pagination;

namespace CRMEngSystem.Models.ViewModels.Enterprise
{
    public sealed class EnterpriseListViewModel : PaginationViewModel
    {
        public IEnumerable<EnterpriseListItemDto> Entities { get; set; } = default!;
        public string? SearchGeneral { get; init; }
        public string? SearchName { get; init; }
        public string? SearchStreet { get; init; }
        public string? SearchRegion { get; init; }
        public string? SearchCity { get; init; }
        public bool? SortAlphabetStreet { get; init; }
        public bool? SortAlphabetCity { get; init; }
        public bool? SortAlphabetRegion { get; init; }
        public bool? SortEnterpriseId { get; init; }
    }
}
