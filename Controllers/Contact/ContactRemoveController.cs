using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.Contact;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Contact
{
    [Authorize]
    public class ContactRemoveController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        public ContactRemoveController(IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager)
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
                return RedirectToAction("ContactDetails", "ContactDetails", new { EntityId });
            }
            TempData["ConfirmModal"] = true;
            TempData["NotifyText"] = "Ви впевнені що хочете видалити цей контакт?";
            return RedirectToAction("ContactDetails", "ContactDetails", new { EntityId });
        }
        public IActionResult OpenNotifyModal()
        {
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Контакт видалено успішно.";
            return RedirectToAction("ContactList", "ContactList");
        }
        public async Task<IActionResult> ConfirmModal(int EntityId)
        {
            var repository = _repositoryFactory.Instantiate<ContactEntity>();
            var contact = await repository.GetEntityAsync(new ContactDataLoader(true, true, true, true, true), contact => contact.ContactId, EntityId);
            var result = await repository.RemoveEntityAsync(contact!);
            TempData["ConfirmModal"] = false;
            return OpenNotifyModal();
        }
    }
}
