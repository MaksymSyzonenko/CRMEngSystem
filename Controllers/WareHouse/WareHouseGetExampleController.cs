using CRMEngSystem.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.WareHouse
{
    [RequireHttps]
    public class WareHouseGetExampleController : Controller
    {
        [HttpGet]
        public IActionResult WareHouseGetExample() => File(Convert.FromBase64String(ConfigurationSettings.WareHouseExample), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Склад_Зразок.xlsx");
    }
}
