using CRMEngSystem.Data.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Account
{
    public class AccountLogOutController : Controller
    {
        private readonly SignInManager<UserEntity> _signInManager;
        public AccountLogOutController(SignInManager<UserEntity> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<IActionResult> AccountLogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("AccountLogin", "AccountLogin");
        }
    }
}
