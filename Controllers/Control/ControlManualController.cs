using CRMEngSystem.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Control
{
    public class ControlManualController : Controller
    {
        public IActionResult ControlManual()
        {
            try
            {
                var fileBytes = Convert.FromBase64String(ConfigurationSettings.Manual);
                // Логирование размера файла
                Console.WriteLine($"File size: {fileBytes.Length}");
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Інструкція.docx");
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
