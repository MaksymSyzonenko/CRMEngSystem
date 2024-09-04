using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Models.ViewModels.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace CRMEngSystem.Controllers.Catalog
{
    [Authorize]
    public class CatalogCreateController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public CatalogCreateController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public IActionResult CatalogCreate(int OrderId, int WareHouseId) => View(new CatalogCreateViewModel
        {
            OrderId = OrderId,
            WareHouseId = WareHouseId
        });
        [HttpPost]
        public async Task<IActionResult> CatalogCreate(CatalogCreateViewModel model)
        {
            EquipmentTypeValue type = EquipmentTypeValue.Main;
            switch (model.Type)
            {
                case "Деталь":
                    type = EquipmentTypeValue.Detail;
                    break;
                case "Ремкомплект":
                    type = EquipmentTypeValue.MaintenanceKit;
                    break;
            }

            byte[] imageBytes = null!;
            if (model.Image != null && model.Image.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await model.Image.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();
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

            var result = await _repositoryFactory.Instantiate<EquipmentCatalogPositionEntity>().AddEntityAsync(new EquipmentCatalogPositionEntity
            {
                EquipmentCode = model.EquipmentCode,
                Image = new CatalogPositionImageEntity
                {
                    Bytes = imageBytes
                },
                NameUA = model.NameUA,
                NameEN = model.NameEN,
                Type = type,
                BasePrice = basePriceResult,
                Weight = weightResult,
                Volume = volumeResult,
                Producer = model.Producer,
                Country = model.Country,
                Link = model.Link,
                DateTimeCreate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time")),
                DateTimeUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time")),
                DateTimeUpdatePrice = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"))
            });
            return OpenModal(model.OrderId, model.WareHouseId);
        }
        public IActionResult OpenModal(int OrderId, int WareHouseId)
        {
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Позицію обладнання створено успішно!";
            return RedirectToAction("CatalogList", "CatalogList", new { OrderId, WareHouseId });
        }
    }
}
