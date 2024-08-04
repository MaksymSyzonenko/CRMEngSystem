using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Models.ViewModels.WareHouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.WareHouse
{
    [Authorize]
    public class WareHouseEditController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public WareHouseEditController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> WareHouseEdit(int WareHouseId)
        {
            var wareHouse = await _repositoryFactory
                .Instantiate<WareHouseEntity>()
                .GetEntityAsync(new WareHouseDataLoader(false), wareHouse => wareHouse.WareHouseId, WareHouseId);

            return View(new WareHouseEditViewModel
            {
                WareHouseId = WareHouseId,
                Name = wareHouse.Name,
                Country = wareHouse.Country,
                City = wareHouse.City,
                Region = wareHouse.Region,
                Street = wareHouse.Street,
                Coordinate = wareHouse.Coordinate,
                PostalCode = wareHouse.PostalCode
            });
        }
        [HttpPost]
        public async Task<IActionResult> WareHouseEdit(WareHouseEditViewModel model)
        {
            var wareHouse = await _repositoryFactory
                .Instantiate<WareHouseEntity>()
                .GetEntityAsync(new WareHouseDataLoader(true), wareHouse => wareHouse.WareHouseId, model.WareHouseId);
            
            wareHouse.Name = model.Name;
            wareHouse.Country = model.Country;
            wareHouse.City = model.City;
            wareHouse.Region = model.Region;
            wareHouse.Street = model.Street;
            wareHouse.Coordinate = model.Coordinate;
            wareHouse.PostalCode = model.PostalCode;

            await _repositoryFactory.Instantiate<WareHouseEntity>().UpdateEntityAsync(wareHouse.WareHouseId, wareHouse);

            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Склад успішно змінено!";
            return RedirectToAction("WareHouseDetails", "WareHouseDetails", new { model.WareHouseId });
        }
    }
}
