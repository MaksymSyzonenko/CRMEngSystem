using CRMEngSystem.Data.Context;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Models.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Account
{
    public class AccountLoginController : Controller
    {
        private readonly SignInManager<UserEntity> _signInManager;
        public AccountLoginController(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult AccountLogin() 
        {
            //_signInManager.SignOutAsync();

            if (User.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AccountLogin(AccountLoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
            if (result.Succeeded)
            {
                return OpenModal();
            }
            else
            {
                TempData["ErrorMessage"] = "Не правильний логін або пароль.";
                return View();
            }
        }
        public IActionResult OpenModal()
        {
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Вхід успішний!";
            return RedirectToAction("Index", "Home");
        }
    }
}
