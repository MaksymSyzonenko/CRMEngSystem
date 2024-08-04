using CRMEngSystem.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Control
{
    public class ControlUpdatePricesGetExampleController : Controller
    {
        [HttpGet]
        public IActionResult ControlUpdatePricesGetExample() => File(Convert.FromBase64String(ConfigurationSettings.UpdatePricesExample), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "ОновленняПрайсів_Зразок.xlsx");
    }
}
