using CRMEngSystem.Services.Authentication;
using CRMEngSystem.Models.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using CRMEngSystem.Data.Enums;
using Microsoft.AspNetCore.Identity;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Data.Loaders.User;
using Microsoft.AspNetCore.Authorization;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Loaders.Contact;

namespace CRMEngSystem.Controllers.Account
{
    [Authorize]
    public class AccountCreateController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly IRepositoryFactory _repositoryFactory;
        public AccountCreateController(UserManager<UserEntity> userManager, IUserAuthenticationService userAuthenticationService, IRepositoryFactory repositoryFactory)
        {
            _userManager = userManager;
            _userAuthenticationService = userAuthenticationService;
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> AccountCreate(int ContactId, string ContactInitials) 
        {
            if ((await _userManager.GetUserAsync(User))!.AccessLevel != AccessLevel.High)
            {
                return OpenErrorModal(ContactId, "Недостатній рівень доступа щоб створити користувача для цього контакту.");
            }
            var users = _repositoryFactory.Instantiate<UserEntity>().GetAllEntitiesAsQueryable(new UserDataLoader(true, false, false));
            var contact = await _repositoryFactory.Instantiate<ContactEntity>().GetEntityAsync(new ContactDataLoader(false, false, true, false, false), contact => contact.ContactId, ContactId);
            if (contact.EnterpriseId != 1)
            {
                return OpenErrorModal(ContactId, "Щоб створити користувача, він повинен бути контактом Stimex Engineering Group.");
            }
            if(users.FirstOrDefault(user => user.ContactId == ContactId) != null)
            {
                return OpenErrorModal(ContactId, "Користувач вже існує для цього контакту.");
            }
            return View(new AccountCreateViewModel { ContactId = ContactId, ContactInitials = ContactInitials }); 
        }

        [HttpPost]
        public async Task<IActionResult> AccountCreate(AccountCreateViewModel model)
        {
            AccessLevel accessLevel = AccessLevel.Low;
            switch (model.AccessLevel)
            {
                case "1 рівень (Максимальний)":
                    accessLevel = AccessLevel.High;
                    break;
                case "2 рівень (Середній)":
                    accessLevel = AccessLevel.Medium;
                    break;
            }
            var result = await _userAuthenticationService.Register(accessLevel, model.ContactId, model.UserName, model.Password);
            if (result)
            {
                return OpenModal(model.ContactId);
            }
            else
            {
                TempData["ErrorMessage"] = "Користувач з таким логіном вже існує, або не дотримані правила створення пароля (Мінімальна довжина 6 символів, одна велика літера, одна цифра).";
                return RedirectToAction("AccountCreate", "AccountCreate", new { model.ContactId, model.ContactInitials });
            }
        }
        public IActionResult OpenErrorModal(int EntityId, string text)
        {
            TempData["ErrorNotifyModal"] = true;
            TempData["NotifyModal"] = false;
            TempData["NotifyText"] = text;
            return RedirectToAction("ContactDetails", "ContactDetails", new { EntityId });
        }
        public IActionResult OpenModal(int EntityId)
        {
            TempData["ErrorNotifyModal"] = false;
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Користувач створений успішно!";
            return RedirectToAction("ContactDetails", "ContactDetails", new { EntityId });
        }
    }
}
