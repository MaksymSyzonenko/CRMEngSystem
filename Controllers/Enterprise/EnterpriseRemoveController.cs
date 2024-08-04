using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.Enterprise;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Enterprise
{
    [Authorize]
    public class EnterpriseRemoveController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        public EnterpriseRemoveController(IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager)
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
                return RedirectToAction("EnterpriseDetails", "EnterpriseDetails", new { EntityId });
            }
            if (EntityId == 1) 
            {
                TempData["NotifyModal"] = false;
                TempData["ErrorNotifyModal"] = true;
                TempData["ConfirmModal"] = false;
                TempData["NotifyText"] = "Це підприємство не може бути видалено.";
                return RedirectToAction("EnterpriseDetails", "EnterpriseDetails", new { EntityId });
            }
            TempData["ConfirmModal"] = true;
            return RedirectToAction("EnterpriseDetails", "EnterpriseDetails", new { EntityId });
        }
        public IActionResult OpenNotifyModal()
        {
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Підприємство видалено успішно.";
            return RedirectToAction("EnterpriseList", "EnterpriseList");
        }
        public async Task<IActionResult> ConfirmModal(int EntityId)
        {
            var repository = _repositoryFactory.Instantiate<EnterpriseEntity>();
            var enterprise = await repository.GetEntityAsync(new EnterpriseDataLoader(true, true, true, true), enterprise => enterprise.EnterpriseId, EntityId);
            var result = await repository.RemoveEntityAsync(enterprise!);
            TempData["ConfirmModal"] = false;
            return OpenNotifyModal();
        }
    }
}
