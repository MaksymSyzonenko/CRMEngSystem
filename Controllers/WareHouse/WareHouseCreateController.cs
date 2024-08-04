using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Models.ViewModels.WareHouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.WareHouse
{
    [Authorize]
    public class WareHouseCreateController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public WareHouseCreateController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public IActionResult WareHouseCreate() => View();
        [HttpPost]
        public async Task<IActionResult> WareHouseCreate(WareHouseCreateViewModel model)
        {
            var result = await _repositoryFactory.Instantiate<WareHouseEntity>().AddEntityAsync(new WareHouseEntity
            {
                Name = model.Name,
                Country = model.Country,
                City = model.City,
                Region = model.Region,
                PostalCode = model.PostalCode,
                Street = model.Street,
                Coordinate = model.Coordinate
            });
            if (result)
                return OpenModal();
            return OpenErrorModal();
        }
        public IActionResult OpenModal()
        {
            TempData["ErrorNotifyModal"] = false;
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Склад створено успішно!";
            return RedirectToAction("WareHouseList", "WareHouseList");
        }
        public IActionResult OpenErrorModal()
        {
            TempData["ErrorNotifyModal"] = true;
            TempData["NotifyModal"] = false;
            TempData["NotifyText"] = "Сталася помилка при створенні складу.";
            return RedirectToAction("WareHouseList", "WareHouseList");
        }
    }
}
