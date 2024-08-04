using CRMEngSystem.Dto.Order;

namespace CRMEngSystem.Models.ViewModels.Contact
{
    public sealed class ContactOrdersViewModel : ContactMenuTabViewModel
    {
        public IEnumerable<OrderListItemDto> Orders { get; set; } = default!;
        public string? OrdersSearchGeneral { get; init; } = default!;
    }
}
