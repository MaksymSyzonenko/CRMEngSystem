using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Data.Loaders.Catalog;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Excel;
using CRMEngSystem.Models.ViewModels.Control;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Controllers.Control
{
    public class ControlUpdatePricesController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public ControlUpdatePricesController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        [HttpPost]
        public async Task<IActionResult> ControlUpdatePrices(ControlUpdatePricesViewModel model)
        {
            if (model.File != null && model.File.Length > 0)
            {
                var filePath = Path.GetTempFileName();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }
                List<(string, decimal)> prices = PriceReader.ReadExcelData(filePath);
                System.IO.File.Delete(filePath);
                var repository = _repositoryFactory.Instantiate<EquipmentCatalogPositionEntity>();
                var equipmentList = await repository.GetAllEntitiesAsQueryable(new EquipmentCatalogPositionDataLoader(true, true, true)).ToListAsync();

                var updatedEntities = new List<EquipmentCatalogPositionEntity>();
                var entityIds = new List<int>();

                foreach (var equipment in equipmentList)
                {
                    var priceUpdate = prices.FirstOrDefault(p => p.Item1 == equipment.EquipmentCode && p.Item1 != "t.b.a.");
                    if (priceUpdate != default)
                    {
                        equipment.BasePrice = priceUpdate.Item2;
                        equipment.DateTimeUpdatePrice = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
                        updatedEntities.Add(equipment);
                        entityIds.Add(equipment.EquipmentCatalogPositionId);
                    }
                }

                if (updatedEntities.Any())
                {
                    await repository.UpdateRangeAsync("EquipmentCatalogPositionId", entityIds, updatedEntities);
                }
            }
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Прайси успішно оновлено!";
            return RedirectToAction("ControlDetails", "ControlDetails");
        }
    }
}
