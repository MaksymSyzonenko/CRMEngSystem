using CRMEngSystem.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Control
{
    [Authorize]
    public class ControlManualController : Controller
    {
        [HttpGet]
        public IActionResult ControlManual() => File(Convert.FromBase64String(ConfigurationSettings.Manual), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Інструкція.docx");
    }
}
