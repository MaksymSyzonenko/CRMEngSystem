using CRMEngSystem.Dto.Enterprise;
using CRMEngSystem.Models.ViewModels.Pagination;

namespace CRMEngSystem.Models.ViewModels.Enterprise
{
    public sealed class EnterpriseChangeViewModel : PaginationViewModel
    {
        public int ContactId { get; set; }
        public string ContactInitials { get; init; } = default!;
        public IEnumerable<EnterpriseChangeDto> Enterprises { get; set; } = default!;
        public string SearchGeneral { get; init; } = default!;
    }
}
