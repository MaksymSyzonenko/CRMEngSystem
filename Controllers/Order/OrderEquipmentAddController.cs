using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderEquipmentAddController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public OrderEquipmentAddController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> OrderEquipmentAdd(int EntityId, int EquipmentOrderPositionId, string ActionName, string ControllerName)
        {
            var repository = _repositoryFactory.Instantiate<EquipmentOrderPositionEntity>();
            var equipment = await repository.GetEntityAsync(new EquipmentOrderPositionDataLoader(false, false), equipment => equipment.EquipmentOrderPositionId, EquipmentOrderPositionId);
            equipment.Quantity += 1;
            await repository.UpdateEntityAsync(equipment.EquipmentOrderPositionId, equipment);
            if(ActionName == "OrderEquipment")
                return RedirectToAction(ActionName, ControllerName, new { EntityId });
            return RedirectToAction(ActionName, ControllerName, new { EntityId = EquipmentOrderPositionId });
        }
    }
}
