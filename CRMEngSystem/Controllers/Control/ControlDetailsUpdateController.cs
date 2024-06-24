using CRMEngSystem.Configuration;
using CRMEngSystem.Models.ViewModels.Control;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace CRMEngSystem.Controllers.Control
{
    [Authorize]
    public class ControlDetailsUpdateController : Controller
    {
        private readonly IConfiguration _configuration;
        public ControlDetailsUpdateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult ControlDetailsUpdate(ControlDetailsViewModel model)
        {
            string currencyCoefficientString = Request.Form["CurrencyCoefficient"]!;
            decimal currencyCoefficientResult = 0;
            if (!string.IsNullOrEmpty(currencyCoefficientString))
            {
                currencyCoefficientString = currencyCoefficientString.Replace(',', '.');
                if (decimal.TryParse(currencyCoefficientString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal currencyCoefficient))
                    currencyCoefficientResult = currencyCoefficient;
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
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            var json = System.IO.File.ReadAllText(filePath);
            var jsonObj = JObject.Parse(json);

            if (currencyCoefficientResult != 0)
            {
                jsonObj["CurrencyCoefficient"] = currencyCoefficientResult;
                ConfigurationSettings.CurrencyCoefficient = currencyCoefficientResult;
            }

            if (shippingRatePerKgResult != 0) 
            {
                jsonObj["ShippingRatePerKg"] = shippingRatePerKgResult;
                ConfigurationSettings.ShippingRatePerKg = shippingRatePerKgResult;
            }

            if (shippingRatePerCubicMeterResult != 0)
            {
                jsonObj["ShippingRatePerCubicMeter"] = shippingRatePerCubicMeterResult;
                ConfigurationSettings.ShippingRatePerCubicMeter = shippingRatePerCubicMeterResult;
            }
            System.IO.File.WriteAllText(filePath, jsonObj.ToString());
            (_configuration as IConfigurationRoot)?.Reload();

            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Значення успішно змінено!";
            TempData["ErrorNotigyModal"] = false;
            return RedirectToAction("ControlDetails", "ControlDetails");
        }
    }
}
