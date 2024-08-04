using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Models.ViewModels.Enterprise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Enterprise
{
    [Authorize]
    public class EnterpriseCreateController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public EnterpriseCreateController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public IActionResult EnterpriseCreate() => View();
        [HttpPost]
        public async Task<IActionResult> EnterpriseCreate(EntepriseCreateViewModel model)
        {
            var result = await _repositoryFactory.Instantiate<EnterpriseEntity>().AddEntityAsync(new EnterpriseEntity
            {
                Details = new EnterpriseDetailsEntity
                {
                    NameUA = model.NameUA,
                    NameEN = model.NameEN,
                    OwnershipFormUA = model.OwnershipFormUA,
                    OwnershipFormEN = model.OwnershipFormEN,
                    IndustryBranch = model.IndustryBranch,
                    TradeGroup = model.TradeGroup,
                    Franchise = model.Franchise,
                    Country = model.Country,
                    City = model.City,
                    Region = model.Region,
                    PostalCode = model.PostalCode,
                    Street = model.Street,
                    Coordinate = model.Coordinate
                },
                DateTimeCreate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time")),
                DateTimeUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"))
            });
            if(result)
                return OpenModal();
            return OpenErrorModal();
        }
        public IActionResult OpenModal()
        {
            TempData["ErrorNotifyModal"] = false;
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Підприємство створено успішно!";
            return RedirectToAction("EnterpriseList", "EnterpriseList");
        }
        public IActionResult OpenErrorModal()
        {
            TempData["ErrorNotifyModal"] = true;
            TempData["NotifyModal"] = false;
            TempData["NotifyText"] = "Сталася помилка при створенні підприємства.";
            return RedirectToAction("EnterpriseList", "EnterpriseList");
        }
    }
}
