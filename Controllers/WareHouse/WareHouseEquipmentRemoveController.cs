using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.WareHouse
{
    [Authorize]
    public class WareHouseEquipmentRemoveController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public WareHouseEquipmentRemoveController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> WareHouseEquipmentRemove(int WareHouseId, int EquipmentWareHousePositionId)
        {
            var repository = _repositoryFactory.Instantiate<EquipmentWareHousePositionEntity>();
            var equipment = await repository.GetEntityAsync(new EquipmentWareHousePositionDataLoader(true, true), equipment => equipment.EquipmentWareHousePositionId, EquipmentWareHousePositionId);
            if(equipment != null)
            {
                if (equipment.Quantity > 1)
                {
                    equipment.Quantity -= 1;
                    await repository.UpdateEntityAsync(equipment.EquipmentWareHousePositionId, equipment);
                }
                else
                {
                    await repository.RemoveEntityAsync(equipment);
                }
            }
            return RedirectToAction("WareHouseDetails", "WareHouseDetails", new { WareHouseId });
        }
    }
}
