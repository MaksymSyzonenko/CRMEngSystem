using CRMEngSystem.Dto.Order;

namespace CRMEngSystem.Models.ViewModels.Order
{
    public sealed class OrderDetailsViewModel : OrderMenuTabViewModel
    {
        public OrderDto Order { get; init; } = default!;
    }
}
