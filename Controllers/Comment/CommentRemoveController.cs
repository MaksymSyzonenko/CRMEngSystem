using CRMEngSystem.Data.Entities.Comment;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.Comment;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Models.ViewModels.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Comment
{
    [Authorize]
    public class CommentRemoveController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IRepositoryFactory _repositoryFactory;
        public CommentRemoveController(UserManager<UserEntity> userManager, IRepositoryFactory repositoryFactory)
        {
            _userManager = userManager;
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> CommentRemove(CommentRemoveViewModel model)
        {
            if ((await _userManager.GetUserAsync(User))!.AccessLevel == AccessLevel.High || (await _userManager.GetUserAsync(User))!.ContactId == model.AuthorId)
            {
                TempData["ConfirmModal"] = true;
                TempData["CommentId"] = model.CommentId;
                return RedirectToAction(model.ActionName, model.ControllerName, new { model.EntityId });
            }
            else
                return OpenErrorModal(model.ActionName, model.ControllerName, model.EntityId);
        }
        public async Task<IActionResult> ConfirmModal(string ActionName, string ControllerName, int EntityId, int CommentId)
        {
            var repository = _repositoryFactory.Instantiate<CommentEntity>();
            var comment = await repository.GetEntityAsync(new CommentDataLoader(true, true, true, true), comment => comment.CommentId, CommentId);
            await repository.RemoveEntityAsync(comment!);
            return OpenModal(ActionName, ControllerName, EntityId);
        }
        public IActionResult OpenModal(string ActionName, string ControllerName, int EntityId)
        {
            TempData["ErrorNotifyModal"] = false;
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Коментар видалено успішно!";
            return RedirectToAction(ActionName, ControllerName, new { EntityId });
        }
        public IActionResult OpenErrorModal(string ActionName, string ControllerName, int EntityId)
        {
            TempData["ErrorNotifyModal"] = true;
            TempData["NotifyModal"] = false;
            TempData["NotifyText"] = "Недостатній рівень доступа для виделання коментаря.";
            return RedirectToAction(ActionName, ControllerName, new { EntityId });
        }
    }
}
