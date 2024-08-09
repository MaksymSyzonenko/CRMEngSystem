using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Loaders.Enterprise;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Models.ViewModels.Enterprise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Enterprise
{
    [Authorize]
    public class EnterpriseEditController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public EnterpriseEditController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> EnterpriseEdit(int EnterpriseId)
        {
            var enterprise = await _repositoryFactory.Instantiate<EnterpriseEntity>().GetEntityAsync(new EnterpriseDataLoader(true, false, false, false), enterprise => enterprise.EnterpriseId, EnterpriseId);
            return View(new EnterpriseEditViewModel
            {
                EnterpriseId = EnterpriseId,
                NameUA = enterprise!.Details.NameUA,
                NameEN = enterprise!.Details.NameEN,
                OwnershipFormUA = enterprise!.Details.OwnershipFormUA,
                OwnershipFormEN = enterprise!.Details.OwnershipFormEN,
                IndustryBranch = enterprise!.Details.IndustryBranch,
                EDRPOU = enterprise!.Details.EDRPOU,
                TradeGroup = enterprise!.Details.TradeGroup,
                Franchise = enterprise!.Details.Franchise!,
                Country = enterprise!.Details.Country,
                Region = enterprise!.Details.Region,
                City = enterprise!.Details.City,
                Street = enterprise!.Details.Street,
                PostalCode = enterprise!.Details.PostalCode,
                Coordinate = enterprise!.Details.Coordinate,
                Link = enterprise!.Details.Link
            });
        }
        [HttpPost]
        public async Task<IActionResult> EnterpriseEdit(EnterpriseEditViewModel model)
        {
            var enterprise = await _repositoryFactory.Instantiate<EnterpriseEntity>().GetEntityAsync(new EnterpriseDataLoader(true, true, true, true), enterprise => enterprise.EnterpriseId, model.EnterpriseId);

            enterprise!.Details.NameUA = model.NameUA;
            enterprise!.Details.NameEN = model.NameEN;
            enterprise!.Details.OwnershipFormUA = model.OwnershipFormUA;
            enterprise!.Details.OwnershipFormEN = model.OwnershipFormEN;
            enterprise!.Details.EDRPOU = model.EDRPOU;
            enterprise!.Details.IndustryBranch = model.IndustryBranch;
            enterprise!.Details.TradeGroup = model.TradeGroup;
            enterprise!.Details.Franchise = model.Franchise!;
            enterprise!.Details.Country = model.Country;
            enterprise!.Details.Region = model.Region;
            enterprise!.Details.City = model.City;
            enterprise!.Details.Street = model.Street;
            enterprise!.Details.PostalCode = model.PostalCode;
            enterprise!.Details.Coordinate = model.Coordinate;
            enterprise!.Details.Link = model.Link;
            enterprise.DateTimeUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));

            var result = await _repositoryFactory.Instantiate<EnterpriseEntity>().UpdateEntityAsync(enterprise!.EnterpriseId, enterprise);
            if (result)
                return OpenModal(model.EnterpriseId);
            return OpenErrorModal(model.EnterpriseId);
        }
        public IActionResult OpenModal(int EntityId)
        {
            TempData["ErrorNotifyModal"] = false;
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Підприємство успішно змінено!";
            return RedirectToAction("EnterpriseDetails", "EnterpriseDetails", new { EntityId });
        }
        public IActionResult OpenErrorModal(int EntityId)
        {
            TempData["ErrorNotifyModal"] = true;
            TempData["NotifyModal"] = false;
            TempData["NotifyText"] = "Сталася помилка при зміні підприємства.";
            return RedirectToAction("EnterpriseDetails", "EnterpriseDetails", new { EntityId });
        }
    }
}
