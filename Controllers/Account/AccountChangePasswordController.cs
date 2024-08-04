using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Models.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Account
{
    [Authorize]
    public class AccountChangePasswordController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        public AccountChangePasswordController(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult AccountChangePassword() => View();
        public async Task<IActionResult> AccountChangePassword(AccountChangePasswordViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (model.OldPassword == model.NewPassword)
            {
                TempData["ErrorMessage"] = "Новий пароль повинен відрізнятися від старого.";
                return RedirectToAction("AccountChangePassword", "AccountChangePassword");
            }
            var result = await _userManager.ChangePasswordAsync(user!, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return OpenModal();
            }
            else
            {
                TempData["ErrorMessage"] = "Не правильний старий пароль, або не дотримані правила створення пароля (Мінімальна довжина 6 символів, одна велика літера, одна цифра).";
                return RedirectToAction("AccountChangePassword", "AccountChangePassword");
            }
        }
        public IActionResult OpenModal()
        {
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Пароль змінено успішно!";
            return RedirectToAction("AccountDetails", "AccountDetails");
        }
    }
}
