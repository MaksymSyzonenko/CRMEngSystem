using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Configuration;
using CRMEngSystem.Models.ViewModels.Control;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CRMEngSystem.Data.Repositories.Factory;
using AutoMapper;
using CRMEngSystem.Dto.Account;
using CRMEngSystem.Data.Loaders.User;
using CRMEngSystem.Attributes.Cache;

namespace CRMEngSystem.Controllers.Control
{
    [Authorize]
    [ServiceFilter(typeof(ClearCacheAttribute))]
    public class ControlDetailsController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        public ControlDetailsController(UserManager<UserEntity> userManager, IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _userManager = userManager;
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> ControlDetails()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(new ControlDetailsViewModel
            {
                Accounts = _mapper.Map<IEnumerable<AccountListItemDto>>(_repositoryFactory.Instantiate<UserEntity>().GetAllEntitiesAsQueryable(new UserDataLoader(true, false, false)).ToList()),
                AccessLevel = user!.AccessLevel,
                CurrentUserId = user.ContactId,
                CurrencyCoefficient = ConfigurationSettings.CurrencyCoefficient,
                CurrencyCoefficient_UAH_EUR = ConfigurationSettings.CurrencyCoefficient_UAH_EUR,
                ShippingRatePerCubicMeter = ConfigurationSettings.ShippingRatePerCubicMeter,
                ShippingRatePerKg = ConfigurationSettings.ShippingRatePerKg
            });
        }
    }
}
