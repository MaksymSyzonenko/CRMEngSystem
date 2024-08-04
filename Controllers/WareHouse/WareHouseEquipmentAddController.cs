using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.WareHouse
{
    [Authorize]
    public class WareHouseEquipmentAddController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public WareHouseEquipmentAddController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> WareHouseEquipmentAdd(int WareHouseId, int EquipmentWareHousePositionId, string SearchGeneral, int CurrentPage)
        {
            var repository = _repositoryFactory.Instantiate<EquipmentWareHousePositionEntity>();
            var equipment = await repository.GetEntityAsync(new EquipmentWareHousePositionDataLoader(true, true), equipment => equipment.EquipmentWareHousePositionId, EquipmentWareHousePositionId);
            equipment.Quantity += 1;
            await repository.UpdateEntityAsync(equipment.EquipmentWareHousePositionId, equipment);
            return RedirectToAction("WareHouseDetails", "WareHouseDetails", new { WareHouseId, SearchGeneral, CurrentPage });
        }
    }
}
