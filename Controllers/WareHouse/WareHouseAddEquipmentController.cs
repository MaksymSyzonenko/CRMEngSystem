using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Models.ViewModels.Catalog;
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
            string? SearchNameEN,
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
            int CurrentPage
        )
        {
            var wareHouse = await _repositoryFactory.Instantiate<WareHouseEntity>().GetEntityAsync(new WareHouseDataLoader(true), wareHouse => wareHouse.WareHouseId, WareHouseId);
            if (wareHouse.EquipmentWareHousePositions.Any(equipment => equipment.EquipmentCatalogPositionId == EquipmentCatalogPositionId))
            {
                TempData["ErrorNotifyModal"] = true;
                TempData["NotifyText"] = "Обладнання вже наявне на складі.";
                return RedirectToAction("CatalogList", "CatalogList", new CatalogListViewModel { WareHouseId = WareHouseId });
            }
            await _repositoryFactory.Instantiate<EquipmentWareHousePositionEntity>().AddEntityAsync(new EquipmentWareHousePositionEntity
            {
                WareHouseId = WareHouseId,
                EquipmentCatalogPositionId = EquipmentCatalogPositionId,
                Quantity = 1
            });
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Обладнання успішно додано до складу!";
            return RedirectToAction("CatalogList", "CatalogList", new CatalogListViewModel 
            {  
                WareHouseId = WareHouseId,
                SearchGeneral = SearchGeneral,
                SearchEquipmentCode = SearchEquipmentCode,
                SearchNameEN = SearchNameEN,
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
