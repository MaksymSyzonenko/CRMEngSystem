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
        public IActionResult ControlUpdatePricesGetExample() 
        {
            byte[] _fileBytes = Convert.FromBase64String(ConfigurationSettings.UpdatePricesExample);
            byte[] file = null;
            using (MemoryStream memoryStream = new())
            {
                memoryStream.Write(_fileBytes, 0, _fileBytes.Length);
                memoryStream.Position = 0;
                file = memoryStream.ToArray();
            }
            return File(file, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "ОновленняПрайсів_Зразок.xlsx");
        } 
    }
}
