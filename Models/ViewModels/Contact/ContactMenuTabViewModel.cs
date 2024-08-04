using CRMEngSystem.Models.ViewModels.Pagination;

namespace CRMEngSystem.Models.ViewModels.Contact
{
    public class ContactMenuTabViewModel : PaginationViewModel
    {
        public int EntityId { get; set; }
        public string ActiveTab { get; set; } = default!;
        public int? NumberOrders { get; set; }
        public int? NumberComments { get; set; }
    }
}
