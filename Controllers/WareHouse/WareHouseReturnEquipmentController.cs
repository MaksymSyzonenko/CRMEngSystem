using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Controllers.WareHouse
{
    [Authorize]
    public class WareHouseReturnEquipmentController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public WareHouseReturnEquipmentController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> WareHouseReturnEquipment(int EquipmentOrderPositionId, int EquipmentCatalogPositionId)
        {
            var repository1 = _repositoryFactory.Instantiate<EquipmentWareHouseOrderEntity>();
            var repository2 = _repositoryFactory.Instantiate<EquipmentWareHousePositionEntity>();

            var positions = await repository1.GetAllEntitiesAsQueryable(new EquipmentWareHouseOrderDataLoader(true, true))
                                             .Where(position => position.EquipmentOrderPositionId == EquipmentOrderPositionId)
                                             .ToListAsync();

            await repository1.RemoveRangeAsync(positions);

            var equipmentWareHousePositions = await repository2.GetAllEntitiesAsQueryable(new EquipmentWareHousePositionDataLoader(true, true))
                                                               .Where(equipment => equipment.EquipmentCatalogPositionId == EquipmentCatalogPositionId)
                                                               .ToListAsync();

            foreach (var position in positions)
            {
                var equipment = equipmentWareHousePositions.FirstOrDefault(e => e.WareHouseId == position.WareHouseId);

                if (equipment != null)
                {
                    equipment.Quantity += position.Quantity;
                    await repository2.UpdateEntityAsync(equipment.EquipmentWareHousePositionId, equipment);
                }
                else
                {
                    var newEquipment = new EquipmentWareHousePositionEntity
                    {
                        WareHouseId = position.WareHouseId,
                        EquipmentCatalogPositionId = EquipmentCatalogPositionId,
                        Quantity = position.Quantity
                    };
                    await repository2.AddEntityAsync(newEquipment);
                }
            }

            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Позиції успішно повернуто на склади!";
            return RedirectToAction("OrderEquipmentDetails", "OrderEquipmentDetails", new { EntityId = EquipmentOrderPositionId });
        }

    }
}
