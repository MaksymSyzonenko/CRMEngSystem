using CRMEngSystem.Dto.Order;

namespace CRMEngSystem.Models.ViewModels.Order
{
    public sealed class OrderEquipmentViewModel : OrderMenuTabViewModel
    {
        public IEnumerable<OrderEquipmentPositionDto> EquipmentPositions { get; set; } = default!;
    }
}
