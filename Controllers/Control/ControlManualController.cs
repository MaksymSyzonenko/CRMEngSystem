using CRMEngSystem.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Control
{
    public class ControlManualController : Controller
    {
        [HttpGet]
        public IActionResult ControlManual()
        {
            var fileBytes = Convert.FromBase64String(ConfigurationSettings.Manual);
            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Інструкція.docx");
        }
    }
}
