using CRMEngSystem.Data.Context;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Models.ViewModels.Enterprise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Enterprise
{
    [Authorize]
    public class EnterpriseCreateController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        private readonly CRMEngSystemDbContext _context;
        public EnterpriseCreateController(IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager, CRMEngSystemDbContext context)
        {
            _repositoryFactory = repositoryFactory;
            _userManager = userManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult EnterpriseCreate() => View();
        [HttpPost]
        public async Task<IActionResult> EnterpriseCreate(EntepriseCreateViewModel model)
        {
            var result = await _repositoryFactory.Instantiate<EnterpriseEntity>().AddEntityGetAsync(new EnterpriseEntity
            {
                Details = new EnterpriseDetailsEntity
                {
                    NameUA = model.NameUA,
                    NameEN = model.NameEN,
                    OwnershipFormUA = model.OwnershipFormUA,
                    OwnershipFormEN = model.OwnershipFormEN,
                    EDRPOU = model.EDRPOU,
                    IndustryBranch = model.IndustryBranch,
                    TradeGroup = model.TradeGroup,
                    Franchise = model.Franchise,
                    Country = model.Country,
                    City = model.City,
                    Region = model.Region,
                    PostalCode = model.PostalCode,
                    Street = model.Street,
                    Coordinate = model.Coordinate,
                    Link = model.Link,
                },
                DateTimeCreate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time")),
                DateTimeUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"))
            });
            if(result != null)
            {
                await _context.EnterpriseSelects.AddAsync(new EnterpriseSelectEntity
                {
                    EnterpriseId = result.EnterpriseId,
                    UserId = _userManager.GetUserId(User)
                });
                await _context.SaveChangesAsync();
                return OpenModal();
            }   
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
