using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.WareHouse
{
    [Authorize]
    public class WareHouseTakeEquipmentController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public WareHouseTakeEquipmentController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> WareHouseTakeEquipment(int Quantity, int WareHouseId, int EquipmentOrderPositionId, int EquipmentCatalogPositionId)
        {
            var repository1 = _repositoryFactory.Instantiate<EquipmentWareHousePositionEntity>();
            var repository2 = _repositoryFactory.Instantiate<EquipmentWareHouseOrderEntity>();

            var equipment = repository1.GetAllEntitiesAsQueryable(new EquipmentWareHousePositionDataLoader(true, true))
                .FirstOrDefault(equipment => equipment.EquipmentCatalogPositionId == EquipmentCatalogPositionId && equipment.WareHouseId == WareHouseId);


            if (equipment == null)
            {
                equipment = new EquipmentWareHousePositionEntity()
                {
                    WareHouseId = WareHouseId,
                    EquipmentCatalogPositionId = EquipmentCatalogPositionId
                };
                await repository1.AddEntityAsync(equipment);
            }

            var position = repository2
                .GetAllEntitiesAsQueryable(new EquipmentWareHouseOrderDataLoader(true, true))
                .FirstOrDefault(position => position.WareHouseId == WareHouseId && position.EquipmentOrderPositionId == EquipmentOrderPositionId);

            if (position != null)
            {
                if(Quantity == 0)
                {
                    equipment.Quantity += position.Quantity;
                    await repository2.RemoveEntityAsync(position);
                }
                else
                {
                    if (equipment.Quantity + position.Quantity == Quantity)
                    {
                        await repository1.RemoveEntityAsync(equipment);
                    }
                    else
                    {
                        equipment.Quantity = equipment.Quantity + position.Quantity - Quantity;
                    }
                    position.Quantity = Quantity;
                    await repository2.UpdateEntityAsync(position.EquipmentWareHouseOrderId, position);
                }
                await repository1.UpdateEntityAsync(equipment.EquipmentWareHousePositionId, equipment);
            }
            else if(position == null && Quantity != 0)
            {
                await repository2.AddEntityAsync(new()
                {
                    Quantity = Quantity,
                    EquipmentOrderPositionId = EquipmentOrderPositionId,
                    WareHouseId = WareHouseId
                });
                if(equipment.Quantity == Quantity) 
                {
                    await repository1.RemoveEntityAsync(equipment);
                }
                else
                {
                    equipment.Quantity -= Quantity;
                    await repository1.UpdateEntityAsync(equipment.EquipmentWareHousePositionId, equipment);
                }
            }

            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = Quantity == 0 ? "Обладнання успішно повернуто на склад!" : TempData["NotifyText"] = "Обладнання успішно взято зі складу!";
            return RedirectToAction("OrderEquipmentDetails", "OrderEquipmentDetails", new { EntityId = EquipmentOrderPositionId });
        }
    }
}
