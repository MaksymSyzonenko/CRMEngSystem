using CRMEngSystem.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Control
{
    public class ControlManualController : Controller
    {
        public IActionResult ControlManual() => File(Convert.FromBase64String(ConfigurationSettings.Manual), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Інструкція.docx");
    }
}
