using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.Catalog;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Excel;
using CRMEngSystem.Models.ViewModels.WareHouse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Controllers.WareHouse
{
    public class WareHouseUpdatePositionsController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public WareHouseUpdatePositionsController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        [HttpPost]
        public async Task<IActionResult> WareHouseUpdatePositions(WareHouseUpdatePositionsViewModel model)
        {
            if (model.File != null && model.File.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }
                List<(string, int)> positions = WareHouseReader.ReadExcelData(filePath);
                System.IO.File.Delete(filePath);
                var updatedEntities = new List<EquipmentWareHousePositionEntity>();
                var entityIds = new List<int>();
                var repository = _repositoryFactory.Instantiate<EquipmentWareHousePositionEntity>();
                var equipmentList = await repository
                    .GetAllEntitiesAsQueryable(new EquipmentWareHousePositionDataLoader(true, true))
                    .Where(equipment => equipment.WareHouseId == model.WareHouseId)
                    .ToListAsync();
                await repository.RemoveRangeAsync(equipmentList);
                foreach (var position in positions)
                {
                    var equipment = _repositoryFactory
                        .Instantiate<EquipmentCatalogPositionEntity>()
                        .GetAllEntitiesAsQueryable(new EquipmentCatalogPositionDataLoader(false, false, false))
                        .FirstOrDefault(equip => equip.EquipmentCode == position.Item1);
                    if(equipment != null)
                    {
                        await repository.AddEntityAsync(new EquipmentWareHousePositionEntity()
                        {
                            WareHouseId = model.WareHouseId,
                            Quantity = position.Item2,
                            EquipmentCatalogPositionId = equipment.EquipmentCatalogPositionId
                        });
                    }
                }

                if (updatedEntities.Any())
                {
                    await repository.UpdateRangeAsync("EquipmentWareHousePositionId", entityIds, updatedEntities);
                }
            }
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Склад успішно заповнено!";
            return RedirectToAction("WareHouseDetails", "WareHouseDetails", new {model.WareHouseId});
        }
    }
}
