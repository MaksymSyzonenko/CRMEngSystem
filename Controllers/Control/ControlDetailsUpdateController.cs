using CRMEngSystem.Configuration;
using CRMEngSystem.Models.ViewModels.Control;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Globalization;
using CRMEngSystem.Services.Settings;

namespace CRMEngSystem.Controllers.Control
{
    [Authorize]
    public class ControlDetailsUpdateController : Controller
    {
        private readonly ISettingsService _settingsService;
        public ControlDetailsUpdateController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }
        public async Task<IActionResult> ControlDetailsUpdate(ControlDetailsViewModel model)
        {
            string currencyCoefficientString = Request.Form["CurrencyCoefficient"]!;
            decimal currencyCoefficientResult = 0;
            if (!string.IsNullOrEmpty(currencyCoefficientString))
            {
                currencyCoefficientString = currencyCoefficientString.Replace(',', '.');
                if (decimal.TryParse(currencyCoefficientString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal currencyCoefficient))
                    currencyCoefficientResult = currencyCoefficient;
            }

            string currencyCoefficient_UAH_EUR_String = Request.Form["CurrencyCoefficient_UAH_EUR"]!;
            decimal currencyCoefficient_UAH_EUR_Result = 0;
            if (!string.IsNullOrEmpty(currencyCoefficient_UAH_EUR_String))
            {
                currencyCoefficient_UAH_EUR_String = currencyCoefficient_UAH_EUR_String.Replace(',', '.');
                if (decimal.TryParse(currencyCoefficient_UAH_EUR_String, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal currencyCoefficient_UAH_EUR))
                    currencyCoefficient_UAH_EUR_Result = currencyCoefficient_UAH_EUR;
            }

            string shippingRatePerKgString = Request.Form["ShippingRatePerKg"]!;
            decimal shippingRatePerKgResult = 0;
            if (!string.IsNullOrEmpty(shippingRatePerKgString))
            {
                shippingRatePerKgString = shippingRatePerKgString.Replace(',', '.');
                if (decimal.TryParse(shippingRatePerKgString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal shippingRatePerKg))
                    shippingRatePerKgResult = shippingRatePerKg;
            }

            string shippingRatePerCubicMeterString = Request.Form["ShippingRatePerCubicMeter"]!;
            decimal shippingRatePerCubicMeterResult = 0;
            if (!string.IsNullOrEmpty(shippingRatePerCubicMeterString))
            {
                shippingRatePerCubicMeterString = shippingRatePerCubicMeterString.Replace(',', '.');
                if (decimal.TryParse(shippingRatePerCubicMeterString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal shippingRatePerCubicMeter))
                    shippingRatePerCubicMeterResult = shippingRatePerCubicMeter;
            }

            if (currencyCoefficientResult != 0)
            {
                await _settingsService.UpdateSettingAsync("CurrencyCoefficient", currencyCoefficientResult);
                ConfigurationSettings.CurrencyCoefficient = currencyCoefficientResult;
            }

            if (currencyCoefficient_UAH_EUR_Result != 0)
            {
                await _settingsService.UpdateSettingAsync("CurrencyCoefficient_UAH_EUR", currencyCoefficient_UAH_EUR_Result);
                ConfigurationSettings.CurrencyCoefficient_UAH_EUR = currencyCoefficient_UAH_EUR_Result;
            }

            if (shippingRatePerKgResult != 0)
            {
                await _settingsService.UpdateSettingAsync("ShippingRatePerKg", shippingRatePerKgResult);
                ConfigurationSettings.ShippingRatePerKg = shippingRatePerKgResult;
            }

            if (shippingRatePerCubicMeterResult != 0)
            {
                await _settingsService.UpdateSettingAsync("ShippingRatePerCubicMeter", shippingRatePerCubicMeterResult);
                ConfigurationSettings.ShippingRatePerCubicMeter = shippingRatePerCubicMeterResult;
            }
            

            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Значення успішно змінено!";
            TempData["ErrorNotigyModal"] = false;
            return RedirectToAction("ControlDetails", "ControlDetails");
        }
    }
}
