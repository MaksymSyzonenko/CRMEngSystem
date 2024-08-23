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
    public class CommentEditController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IRepositoryFactory _repositoryFactory;
        public CommentEditController(UserManager<UserEntity> userManager, IRepositoryFactory repositoryFactory)
        {
            _userManager = userManager;
            _repositoryFactory = repositoryFactory;
        }
        [HttpPost]
        public async Task<IActionResult> CommentEdit(EditCommentViewModel model)
        {
            if ((await _userManager.GetUserAsync(User))!.AccessLevel == AccessLevel.High || (await _userManager.GetUserAsync(User))!.ContactId == model.AuthorId)
            {
                var repository = _repositoryFactory.Instantiate<CommentEntity>();
                var comment = await repository.GetEntityAsync(new CommentDataLoader(false, false, false, false), comment => comment.CommentId, model.CommentId);
                comment.Text = model.NewText;
                await repository.UpdateEntityAsync(model.CommentId, comment);
                TempData["NotifyModal"] = true;
                TempData["NotifyText"] = "Коментар успішно змінено!";
                return RedirectToAction(model.ActionName, model.ControllerName, new { model.EntityId });
            }
            else
            {
                TempData["ErrorNotifyModal"] = true;
                TempData["NotifyText"] = "Недостатній рівень доступа для зміни коментаря.";
                return RedirectToAction(model.ActionName, model.ControllerName, new { model.EntityId }); 
            }
        }
    }
}
