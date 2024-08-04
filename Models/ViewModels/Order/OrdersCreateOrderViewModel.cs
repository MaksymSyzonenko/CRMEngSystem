using CRMEngSystem.Dto.Order;

namespace CRMEngSystem.Models.ViewModels.Order
{
    public sealed class OrdersCreateOrderViewModel
    {
        public List<OrdersCreateOrderDto> Orders { get; init; } = default!;
    }
}
