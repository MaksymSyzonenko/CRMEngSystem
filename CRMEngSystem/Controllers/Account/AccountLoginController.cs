using CRMEngSystem.Data.Context;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Models.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Account
{
    public class AccountLoginController : Controller
    {
        private readonly CRMEngSystemDbContext _context; 
        private readonly SignInManager<UserEntity> _signInManager;
        public AccountLoginController(CRMEngSystemDbContext context, SignInManager<UserEntity> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult AccountLogin() 
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            _signInManager.SignOutAsync();


            if (User.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AccountLogin(AccountLoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, lockoutOnFailure: false);
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
