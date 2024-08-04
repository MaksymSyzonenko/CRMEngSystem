using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.WareHouse
{
    [Authorize]
    public class WareHouseRemoveController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        public WareHouseRemoveController(IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager)
        {
            _repositoryFactory = repositoryFactory;
            _userManager = userManager;
        }
        public async Task<IActionResult> OpenModal(int WareHouseId)
        {
            if ((await _userManager.GetUserAsync(User))!.AccessLevel == AccessLevel.Low)
            {
                TempData["ErrorNotifyModal"] = true;
                TempData["NotifyText"] = "Недостатній рівень доступа для виконання дії.";
                return RedirectToAction("WareHouseDetails", "WareHouseDetails", new { WareHouseId });
            }
            TempData["ConfirmModal"] = true;
            return RedirectToAction("WareHouseDetails", "WareHouseDetails", new { WareHouseId });
        }
        public IActionResult OpenNotifyModal()
        {
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Склад видалено успішно.";
            return RedirectToAction("WareHouseList", "WareHouseList");
        }
        public async Task<IActionResult> ConfirmModal(int WareHouseId)
        {
            var repository = _repositoryFactory.Instantiate<WareHouseEntity>();
            var enterprise = await repository.GetEntityAsync(new WareHouseDataLoader(true), wareHouse => wareHouse.WareHouseId, WareHouseId);
            var result = await repository.RemoveEntityAsync(enterprise!);
            TempData["ConfirmModal"] = false;
            return OpenNotifyModal();
        }
    }
}
