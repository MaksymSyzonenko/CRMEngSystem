using CRMEngSystem.Attributes.Cache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.PatchNotes
{
    [Authorize]
    [ServiceFilter(typeof(ClearCacheAttribute))]
    public class PatchNotesController : Controller
    {
        public IActionResult PatchNotes() => View();
    }
}
