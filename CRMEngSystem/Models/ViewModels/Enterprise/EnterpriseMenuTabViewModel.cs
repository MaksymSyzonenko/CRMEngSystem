using CRMEngSystem.Models.ViewModels.Pagination;

namespace CRMEngSystem.Models.ViewModels.Enterprise
{
    public class EnterpriseMenuTabViewModel : PaginationViewModel
    {
        public int EntityId { get; init; }
        public string ActiveTab { get; init; } = default!;
        public int? NumberOrders { get; init; }
        public int? NumberContacts { get; init; }
        public int? NumberComments { get; init; }
    }
}
