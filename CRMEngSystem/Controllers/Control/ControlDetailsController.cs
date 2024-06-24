using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Configuration;
using CRMEngSystem.Models.ViewModels.Control;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Control
{
    [Authorize]
    public class ControlDetailsController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        public ControlDetailsController(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> ControlDetails()
        {
            return View(new ControlDetailsViewModel
            {
                AccessLevel = (await _userManager.GetUserAsync(User))!.AccessLevel,
                CurrencyCoefficient = ConfigurationSettings.CurrencyCoefficient,
                ShippingRatePerCubicMeter = ConfigurationSettings.ShippingRatePerCubicMeter,
                ShippingRatePerKg = ConfigurationSettings.ShippingRatePerKg
            });
        }
        public IActionResult CloseModal()
        {
            TempData["NotifyModal"] = false;
            TempData["ErorrNotifyModal"] = false;
            return RedirectToAction("ControlDetails", "ControlDetails");
        }
    }
}
