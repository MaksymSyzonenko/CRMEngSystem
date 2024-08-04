using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderEquipmentRemoveController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public OrderEquipmentRemoveController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> OrderEquipmentRemove(int EntityId, int EquipmentOrderPositionId, string ActionName, string ControllerName)
        {
            var repository1 = _repositoryFactory.Instantiate<EquipmentOrderPositionEntity>();
            var repository2 = _repositoryFactory.Instantiate<EquipmentWareHouseOrderEntity>();
            var equipment = await repository1.GetEntityAsync(new EquipmentOrderPositionDataLoader(false, false), equipment => equipment.EquipmentOrderPositionId, EquipmentOrderPositionId);
            int quantity = repository2
                        .GetAllEntitiesAsQueryable(new EquipmentWareHouseOrderDataLoader(true, true))
                        .Where(equip => equip.EquipmentOrderPositionId == EquipmentOrderPositionId)
                        .Sum(equip => equip.Quantity);
            if (equipment != null)
            {
                if(!(equipment.Quantity - 1 < quantity))
                {
                    if (equipment.Quantity > 1)
                    {
                        equipment.Quantity -= 1;
                        await repository1.UpdateEntityAsync(equipment.EquipmentOrderPositionId, equipment);
                    }
                    else
                    {
                        await repository1.RemoveEntityAsync(equipment);
                        return RedirectToAction("OrderEquipment", "OrderEquipment", new { EntityId });
                    }
                }
                else
                {
                    TempData["ErrorNotifyModal"] = true;
                    TempData["NotifyText"] = "Не можна зменшити кількість, поки кількість взятого обладнання зі складів перевищено.";
                }
            }
            if (ActionName == "OrderEquipment")
                return RedirectToAction(ActionName, ControllerName, new { EntityId });
            return RedirectToAction(ActionName, ControllerName, new { EntityId = EquipmentOrderPositionId });
        }
    }
}
