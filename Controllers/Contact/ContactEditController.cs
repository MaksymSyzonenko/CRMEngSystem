using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Loaders.Contact;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Models.ViewModels.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Contact
{
    [Authorize]
    public class ContactEditController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public ContactEditController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> ContactEdit(int ContactId)
        {
            var contact = await _repositoryFactory.Instantiate<ContactEntity>().GetEntityAsync(new ContactDataLoader(true, true, true, false, false), contact => contact.ContactId, ContactId);
            return View(new ContactEditViewModel 
            {
                ContactId = ContactId,
                EnterpriseId = contact!.EnterpriseId,
                FirstName = contact!.Details.FirstName,
                LastName = contact!.Details.LastName,
                MiddleName = contact!.Details.MiddleName,
                Position = contact!.Details.Position,
                FirstPhoneNumber = contact!.Details.FirstPhoneNumber,
                ExtraPhoneNumber = contact!.Details.ExtraPhoneNumber,
                FirstEmail = contact!.Details.FirstEmail,
                ExtraEmail = contact!.Details.ExtraEmail,
                TelegramLink = contact!.Details.TelegramLink,
                LinkedInLink = contact!.Details.LinkedInLink,
                ViberLink = contact!.Details.ViberLink,
                FaceBookLink = contact!.Details.FaceBookLink,
                WhatsappLink = contact!.Details.WhatsappLink,
                SkypeLink = contact!.Details.SkypeLink,
                TwitterLink = contact!.Details.TwitterLink,
                InstagramLink = contact!.Details.InstagramLink
            });
        }
        [HttpPost]
        public async Task<IActionResult> ContactEdit(ContactEditViewModel model)
        {
            var contact = await _repositoryFactory.Instantiate<ContactEntity>().GetEntityAsync(new ContactDataLoader(true, true, true, true, true), contact => contact.ContactId, model.ContactId);
            byte[] imageBytes = null!;
            if (model.Image != null && model.Image.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await model.Image.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();
                contact!.Image!.Bytes = imageBytes;
            }

            contact.Details.LastName = model.LastName;
            contact.Details.FirstName = model.FirstName;
            contact.Details.MiddleName = model.MiddleName;
            contact.Details.Position = model.Position;
            contact.Details.FirstPhoneNumber = model.FirstPhoneNumber;
            contact.Details.ExtraPhoneNumber = model.ExtraPhoneNumber;
            contact.Details.FirstEmail = model.FirstEmail;
            contact.Details.ExtraEmail = model.ExtraEmail;
            contact.Details.TelegramLink = model.TelegramLink;
            contact.Details.LinkedInLink = model.LinkedInLink;
            contact.Details.ViberLink = model.ViberLink;
            contact.Details.FaceBookLink = model.FaceBookLink;
            contact.Details.WhatsappLink = model.WhatsappLink;
            contact.Details.SkypeLink = model.SkypeLink;
            contact.Details.TwitterLink = model.TwitterLink;
            contact.Details.InstagramLink = model.InstagramLink;
            contact.DateTimeUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
            var result = await _repositoryFactory.Instantiate<ContactEntity>().UpdateEntityAsync(contact!.ContactId, contact);
            if (result)
                return OpenModal(model.ContactId);
            return OpenErrorModal(model.ContactId);
        }
        public IActionResult OpenModal(int EntityId)
        {
            TempData["ErrorNotifyModal"] = false;
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Контакт успішно змінено!";
            return RedirectToAction("ContactDetails", "ContactDetails", new { EntityId });
        }
        public IActionResult OpenErrorModal(int EntityId)
        {
            TempData["ErrorNotifyModal"] = true;
            TempData["NotifyModal"] = false;
            TempData["NotifyText"] = "Сталася помилка при зміні контакта.";
            return RedirectToAction("ContactDetails", "ContactDetails", new { EntityId });
        }
    }
}
