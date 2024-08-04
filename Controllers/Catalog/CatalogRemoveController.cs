using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.Catalog;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Catalog
{
    [Authorize]
    public class CatalogRemoveController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        public CatalogRemoveController(IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager)
        {
            _repositoryFactory = repositoryFactory;
            _userManager = userManager;
        }
        public async Task<IActionResult> OpenModal(int EntityId)
        {
            if ((await _userManager.GetUserAsync(User))!.AccessLevel == AccessLevel.Low)
            {
                TempData["ErrorNotifyModal"] = true;
                TempData["NotifyText"] = "Недостатній рівень доступа для виконання дії.";
                return RedirectToAction("CatalogDetails", "CatalogDetails", new { EntityId });
            }
            TempData["ConfirmModal"] = true;
            TempData["NotifyText"] = "Ви впевнені що хочете видалити цю позицію каталога?";
            return RedirectToAction("CatalogDetails", "CatalogDetails", new { EntityId });
        }
        public IActionResult OpenNotifyModal()
        {
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Позиція каталога видалена успішно.";
            return RedirectToAction("CatalogList", "CatalogList");
        }
        public async Task<IActionResult> ConfirmModal(int EntityId)
        {
            var repository = _repositoryFactory.Instantiate<EquipmentCatalogPositionEntity>();
            var equipment = await repository.GetEntityAsync(new EquipmentCatalogPositionDataLoader(true, true, true), equipment => equipment.EquipmentCatalogPositionId, EntityId);
            var result = await repository.RemoveEntityAsync(equipment!);
            TempData["ConfirmModal"] = false;
            return OpenNotifyModal();
        }
    }
}
