using AutoMapper;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Loaders.Enterprise;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Models.ViewModels.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Contact
{
    [Authorize]
    public class ContactCreateController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public ContactCreateController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> ContactCreate(int EnterpriseId)
        {
            var enterprise = await _repositoryFactory.Instantiate<EnterpriseEntity>().GetEntityAsync(new EnterpriseDataLoader(true, false, false, false), entity => entity.EnterpriseId, EnterpriseId);
            return View(new ContactCreateViewModel { EnterpriseId = EnterpriseId, EnterpriseNameUA = enterprise!.Details.NameUA });
        }
        [HttpPost]
        public async Task<IActionResult> ContactCreate(ContactCreateViewModel model)
        {
            byte[] imageBytes = null!;
            if (model.Image != null && model.Image.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await model.Image.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();
            }
            var result = await _repositoryFactory.Instantiate<ContactEntity>().AddEntityAsync(new ContactEntity
            {
                EnterpriseId = model.EnterpriseId,
                Image = new ContactImageEntity
                {
                    Bytes = imageBytes
                },
                Details = new ContactDetailsEntity
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    Position = model.Position,
                    FirstPhoneNumber = model.FirstPhoneNumber,
                    ExtraPhoneNumber = model.ExtraPhoneNumber,
                    FirstEmail = model.FirstEmail,
                    ExtraEmail = model.ExtraEmail,
                    TelegramLink = model.TelegramLink,
                    LinkedInLink = model.LinkedInLink,
                    ViberLink = model.ViberLink,
                    FaceBookLink = model.FaceBookLink,
                    WhatsappLink = model.WhatsappLink,
                    SkypeLink = model.SkypeLink,
                    TwitterLink = model.TwitterLink,
                    InstagramLink = model.InstagramLink
                },
                DateTimeCreate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time")),
                DateTimeUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"))
            });
            if (result)
                return OpenModal(model.EnterpriseId);
            return OpenErrorModal(model.EnterpriseId);
        }
        public IActionResult OpenModal(int EntityId)
        {
            TempData["ErrorNotifyModal"] = false;
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Контакт успішно створено!";
            return EntityId != 1 ? RedirectToAction("EnterpriseDetails", "EnterpriseDetails", new { EntityId }) : RedirectToAction("EnterpriseContacts", "EnterpriseContacts", new { EntityId });
        }
        public IActionResult OpenErrorModal(int EntityId)
        {
            TempData["ErrorNotifyModal"] = true;
            TempData["NotifyModal"] = false;
            TempData["NotifyText"] = "Помилка при створенні контакта.";
            return EntityId != 1 ? RedirectToAction("EnterpriseDetails", "EnterpriseDetails", new { EntityId }) : RedirectToAction("EnterpriseContacts", "EnterpriseContacts", new { EntityId });
        }
    }
}
