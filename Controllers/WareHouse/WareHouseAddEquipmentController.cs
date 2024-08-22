using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Models.ViewModels.Catalog;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.WareHouse
{
    [Authorize]
    public class WareHouseAddEquipmentController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public WareHouseAddEquipmentController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> WareHouseAddEquipment(
            int WareHouseId,
            int EquipmentCatalogPositionId,
            string? SearchGeneral,
            string? SearchEquipmentCode,
            string? SearchName,
            string? FilterType,
            decimal? FilterMinBasePrice,
            decimal? FilterMaxBasePrice,
            decimal? FilterMinWeight,
            decimal? FilterMaxWeight,
            decimal? FilterMinVolume,
            decimal? FilterMaxVolume,
            bool? SortCode,
            bool? SortAlphabetNameEN,
            bool? SortPrice,
            bool? SortWeight,
            bool? SortVolume,
            int CurrentPage,
            int Quantity
        )
        {
            var wareHouse = await _repositoryFactory.Instantiate<WareHouseEntity>().GetEntityAsync(new WareHouseDataLoader(true), wareHouse => wareHouse.WareHouseId, WareHouseId);
            if (wareHouse.EquipmentWareHousePositions.Any(equipment => equipment.EquipmentCatalogPositionId == EquipmentCatalogPositionId))
            {
                var equipment = wareHouse.EquipmentWareHousePositions.FirstOrDefault(equipment => equipment.EquipmentCatalogPositionId == EquipmentCatalogPositionId);
                equipment.Quantity = Quantity;
                await _repositoryFactory
                    .Instantiate<EquipmentWareHousePositionEntity>()
                    .UpdateEntityAsync(equipment.EquipmentWareHousePositionId, equipment);
                TempData["NotifyModal"] = true;
                TempData["NotifyText"] = "Обладнання успішно замінено для складу!";
            }
            else
            {
                await _repositoryFactory.Instantiate<EquipmentWareHousePositionEntity>().AddEntityAsync(new EquipmentWareHousePositionEntity
                {
                    WareHouseId = WareHouseId,
                    EquipmentCatalogPositionId = EquipmentCatalogPositionId,
                    Quantity = Quantity
                });
                TempData["NotifyModal"] = true;
                TempData["NotifyText"] = "Обладнання успішно додано до складу!";
            }
            return RedirectToAction("CatalogList", "CatalogList", new CatalogListViewModel 
            {  
                WareHouseId = WareHouseId,
                SearchGeneral = SearchGeneral,
                SearchEquipmentCode = SearchEquipmentCode,
                SearchName = SearchName,
                FilterType = FilterType,
                FilterMinBasePrice = FilterMinBasePrice,
                FilterMaxBasePrice = FilterMaxBasePrice,
                FilterMinWeight = FilterMinWeight,
                FilterMaxWeight = FilterMaxWeight,
                FilterMinVolume = FilterMinVolume,
                FilterMaxVolume = FilterMaxVolume,
                SortCode = SortCode,
                SortAlphabetNameEN = SortAlphabetNameEN,
                SortPrice = SortPrice,
                SortWeight = SortWeight,
                SortVolume = SortVolume,
                CurrentPage = CurrentPage
            });
        }
    }
}
