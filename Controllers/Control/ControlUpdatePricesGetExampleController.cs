using CRMEngSystem.Configuration;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Control
{
    [Authorize]
    public class ControlUpdatePricesGetExampleController : Controller
    {
        [HttpGet]
        public IActionResult ControlUpdatePricesGetExample() => File(Convert.FromBase64String(ConfigurationSettings.UpdatePricesExample), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "ОновленняПрайсів_Зразок.xlsx");
    }
}
