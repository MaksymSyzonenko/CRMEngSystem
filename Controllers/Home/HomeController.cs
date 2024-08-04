using AutoMapper;
using CRMEngSystem.Configuration;
using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Extensions;
using CRMEngSystem.Data.Loaders.Catalog;
using CRMEngSystem.Data.Loaders.Contact;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Contact;
using CRMEngSystem.Dto.Order;
using CRMEngSystem.Models;
using CRMEngSystem.Models.ViewModels.Home;
using CRMEngSystem.Services.Calculate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Globalization;

namespace CRMEngSystem.Controllers.Home
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly UserManager<UserEntity> _userManager;
        public HomeController(IMapper mapper, IRepositoryFactory repositoryFactory, IMemoryCache memoryCache, UserManager<UserEntity> userManager)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
            _memoryCache = memoryCache;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(HomeViewModel model)
        {
            var contactName = await _memoryCache.GetOrCreateAsync("contactName", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                var user = await _userManager.GetUserAsync(User);
                var contact = await _repositoryFactory
                    .Instantiate<ContactEntity>()
                    .GetEntityAsync(new ContactDataLoader(true, false, false, false, false),
                                    contact => contact.ContactId,
                                    user!.ContactId);

                return contact!.Details.FirstName;
            });

            var ordersList = await _memoryCache.GetOrCreateAsync("ordersList", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);

                var orderRepository = _repositoryFactory.Instantiate<OrderEntity>();
                var orders = await orderRepository
                    .GetAllEntitiesAsQueryable(new OrderDataLoader(true, false, true, false, false))
                    .OrderByDescending(order => order.OrderId)
                    .Take(12)
                    .ToListAsync();

                return orders;
            });

            var contactsList = await _memoryCache.GetOrCreateAsync("contactsList", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);

                var contacts = ordersList!
                    .SelectMany(order => order.ContactOrders)
                    .Where(contactOrder => contactOrder.Contact.EnterpriseId != 1)
                    .Select(contactOrder => contactOrder.Contact)
                    .Distinct()
                    .Take(12)
                    .ToList();

                return Task.FromResult(_mapper.Map<IEnumerable<ContactListItemDto>>(contacts));
            });

            model.UserName = contactName!;
            model.Orders = _mapper.Map<IEnumerable<OrderListItemDto>>(ordersList) ?? Enumerable.Empty<OrderListItemDto>();
            model.Contacts = contactsList ?? Enumerable.Empty<ContactListItemDto>();

            var currencyCalculator = new CurrencyCalculator(_memoryCache);
            await currencyCalculator.Initialize();

            model.GBP_Value = model.GBP_Value != 0 ? model.GBP_Value : 1;
            model.EUR_Value = model.EUR_Value != 0 ? model.EUR_Value : model.GBP_Value * currencyCalculator.GBP_EUR_Currency;
            model.USD_Value = model.USD_Value != 0 ? model.USD_Value : model.GBP_Value * currencyCalculator.GBP_USD_Currency;
            model.UAH_Value = model.UAH_Value != 0 ? model.UAH_Value : model.GBP_Value * currencyCalculator.UAH_GBP_Currency;
            model.UAH_GBP_Currency = currencyCalculator.UAH_GBP_Currency;
            model.UAH_EUR_Currency = currencyCalculator.UAH_EUR_Currency;
            model.UAH_USD_Currency = currencyCalculator.UAH_USD_Currency;
            model.UAH_GBP_RateChanges = currencyCalculator.UAH_GBP_RateChanges;
            model.UAH_EUR_RateChanges = currencyCalculator.UAH_EUR_RateChanges;
            model.UAH_USD_RateChanges = currencyCalculator.UAH_USD_RateChanges;

            return View(model);
        }

        [HttpPost]
        public IActionResult FindPriceEquipmentCode(HomeViewModel model)
        {
            var equipment = _repositoryFactory
                .Instantiate<EquipmentCatalogPositionEntity>()
                .GetAllEntitiesAsQueryable(new EquipmentCatalogPositionDataLoader(false, false, false))
                .FirstOrDefault(equipment => equipment.EquipmentCode == model.EquipmentCode);

            model.BasePrice = equipment != null ? equipment.BasePrice : 0;
            model.EquipmentNameEN = equipment != null ? equipment.NameEN : string.Empty;

            return RedirectToAction("Index", "Home", model);
        }

        [HttpPost]
        public IActionResult CalculatePercentValues(HomeViewModel model)
        {
            string basePriceString = Request.Form["BasePrice"]!;
            decimal basePriceResult = 0;
            if (!string.IsNullOrEmpty(basePriceString))
            {
                basePriceString = basePriceString.Replace(',', '.');
                if (decimal.TryParse(basePriceString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal basePrice))
                    basePriceResult = basePrice;
            }

            model.BasePrice = basePriceResult;
            model.PurchasePrice = PriceCalculator.CalculatePurchasePrice(model.Discount, basePriceResult) * ConfigurationSettings.CurrencyCoefficient;
            model.SellPrice = PriceCalculator.CalculateSellPrice(model.Discount, model.MarkUp, basePriceResult) * ConfigurationSettings.CurrencyCoefficient;

            return RedirectToAction("Index", "Home", model);
        }

        [HttpPost]
        public async Task<IActionResult> CalculateCurrency(HomeViewModel model)
        {
            var currencyCalculator = new CurrencyCalculator(_memoryCache);
            await currencyCalculator.Initialize();
            decimal gbp_value = 0;
            decimal eur_value = 0;
            decimal usd_value = 0;
            decimal uah_value = 0;

            switch (model.Currency)
            {
                case "GBP":
                    gbp_value = model.Value;
                    eur_value = model.Value * currencyCalculator.GBP_EUR_Currency;
                    usd_value = model.Value * currencyCalculator.GBP_USD_Currency;
                    uah_value = model.Value * currencyCalculator.UAH_GBP_Currency;
                    break;
                case "EUR":
                    eur_value = model.Value;
                    gbp_value = model.Value * currencyCalculator.EUR_GBP_Currency;
                    usd_value = model.Value * currencyCalculator.EUR_USD_Currency;
                    uah_value = model.Value * currencyCalculator.UAH_EUR_Currency;
                    break;
                case "USD":
                    usd_value = model.Value;
                    gbp_value = model.Value * currencyCalculator.USD_GBP_Currency;
                    eur_value = model.Value * currencyCalculator.USD_EUR_Currency;
                    uah_value = model.Value * currencyCalculator.UAH_USD_Currency;
                    break;
                case "UAH":
                    uah_value = model.Value;
                    gbp_value = model.Value / currencyCalculator.UAH_GBP_Currency;
                    eur_value = model.Value / currencyCalculator.UAH_EUR_Currency;
                    usd_value = model.Value / currencyCalculator.UAH_USD_Currency;
                    break;
            }

            model.GBP_Value = gbp_value;
            model.EUR_Value = eur_value;
            model.USD_Value = usd_value;
            model.UAH_Value = uah_value;

            return RedirectToAction("Index", "Home", model);
        }

    public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}