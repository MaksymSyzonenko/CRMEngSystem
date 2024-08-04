using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.Catalog;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Models.ViewModels.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CRMEngSystem.Controllers.Catalog
{
    [Authorize]
    public class CatalogEditController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public CatalogEditController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> CatalogEdit(int EquipmentCatalogPositionId)
        {
            var equipment = await _repositoryFactory.Instantiate<EquipmentCatalogPositionEntity>().GetEntityAsync(new EquipmentCatalogPositionDataLoader(true, false, false), equipment => equipment.EquipmentCatalogPositionId, EquipmentCatalogPositionId);
            return View(new CatalogEditViewModel
            {
                EquipmentCatalogPositionId = EquipmentCatalogPositionId,
                EquipmentCode = equipment.EquipmentCode,
                NameUA = equipment.NameUA,
                NameEN = equipment.NameEN,
                Type = equipment.Type.ToString(),
                BasePrice = equipment.BasePrice,
                Weight = equipment.Weight,
                Volume = equipment.Volume,
                Producer = equipment.Producer,
                Country = equipment.Country,
                Link = equipment.Link
            });
        }
        [HttpPost]
        public async Task<IActionResult> CatalogEdit(CatalogEditViewModel model)
        {
            var equipment = await _repositoryFactory.Instantiate<EquipmentCatalogPositionEntity>().GetEntityAsync(new EquipmentCatalogPositionDataLoader(true, true, true), equipment => equipment.EquipmentCatalogPositionId, model.EquipmentCatalogPositionId);
            byte[] imageBytes = null!;
            if (model.Image != null && model.Image.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await model.Image.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();
                equipment!.Image!.Bytes = imageBytes;
            }

            string basePriceString = Request.Form["BasePrice"]!;
            decimal basePriceResult = 0;
            if (!string.IsNullOrEmpty(basePriceString))
            {
                basePriceString = basePriceString.Replace(',', '.');
                if (decimal.TryParse(basePriceString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal basePrice))
                    basePriceResult = basePrice;
            }

            string weightString = Request.Form["Weight"]!;
            decimal weightResult = 0;
            if (!string.IsNullOrEmpty(weightString))
            {
                weightString = weightString.Replace(',', '.');
                if (decimal.TryParse(weightString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal weight))
                    weightResult = weight;
            }

            string volumeString = Request.Form["Volume"]!;
            decimal volumeResult = 0;
            if (!string.IsNullOrEmpty(volumeString))
            {
                volumeString = volumeString.Replace(',', '.');
                if (decimal.TryParse(volumeString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal volume))
                    volumeResult = volume;
            }

            if (basePriceResult != 0)
                equipment.BasePrice = basePriceResult;

            if (weightResult != 0)
                equipment.Weight = weightResult;

            if (volumeResult != 0)
                equipment.Volume = volumeResult;

            equipment.EquipmentCode = model.EquipmentCode;
            equipment.NameUA = model.NameUA;
            equipment.NameEN = model.NameEN;
            EquipmentTypeValue type = EquipmentTypeValue.Main;
            switch (model.Type)
            {
                case "Detail":
                    type = EquipmentTypeValue.Detail;
                    break;
                case "MaintenanceKit":
                    type = EquipmentTypeValue.MaintenanceKit;
                    break;
            }
            equipment.Type = type;
            equipment.Producer = model.Producer;
            equipment.Country = model.Country;
            equipment.Link = model.Link;
            equipment.DateTimeUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
            var result = await _repositoryFactory.Instantiate<EquipmentCatalogPositionEntity>().UpdateEntityAsync(model.EquipmentCatalogPositionId, equipment);
            return OpenModal(model.EquipmentCatalogPositionId);

        }
        public IActionResult OpenModal(int EntityId)
        {
            TempData["ErrorNotifyModal"] = false;
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Обладнання успішно змінено!";
            return RedirectToAction("CatalogDetails", "CatalogDetails", new { EntityId });
        }
    }
}
