using CRMEngSystem.Dto.Order;
using CRMEngSystem.Dto.WareHouse;

namespace CRMEngSystem.Models.ViewModels.Order
{
    public class OrderEquipmentDetailsViewModel
    {
        public OrderEquipmentPositionDetailsDto OrderPosition { get; init; } = default!;
        public IEnumerable<EquipmentWareHouseOrderDto> EquipmentWareHouseOrderList { get; init; } = default!;
        public int QuantityWareHouse { get; init; }
    }
}
