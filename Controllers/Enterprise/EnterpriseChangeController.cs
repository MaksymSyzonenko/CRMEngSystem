using AutoMapper;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Loaders.Contact;
using CRMEngSystem.Data.Loaders.Enterprise;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Enterprise;
using CRMEngSystem.Models.ViewModels.Enterprise;
using CRMEngSystem.Services.Search.Enterprise;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Controllers.Enterprise
{
    public class EnterpriseChangeController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        public EnterpriseChangeController(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> EnterpriseChangeAsync(EnterpriseChangeViewModel model)
        {
            var enterprises = _repositoryFactory.Instantiate<EnterpriseEntity>().GetAllEntitiesAsQueryable(new EnterpriseDataLoader(true, false, false, false));

            var searchService = new EnterpriseSearchService(model.SearchGeneral, null, null, null, null);
            enterprises = searchService.Search(enterprises);

            model.TotalPageCount = (int)Math.Ceiling((decimal)enterprises.Count() / model.NumberItemsPerPage);
            enterprises = enterprises.Skip((model.CurrentPage - 1) * model.NumberItemsPerPage).Take(model.NumberItemsPerPage);
            model.Enterprises = _mapper.Map<IEnumerable<EnterpriseChangeDto>>(await enterprises.ToListAsync());

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EnterpriseChangeAsync(int ContactId, int EnterpriseId)
        {
            var repository = _repositoryFactory.Instantiate<ContactEntity>();
            var contact = await repository
                .GetEntityAsync(new ContactDataLoader(true, true, true, true, true), contact => contact.ContactId, ContactId);

            contact.EnterpriseId = EnterpriseId;
            contact.DateTimeUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));

            await repository.UpdateEntityAsync(ContactId, contact);

            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Підприємство успішно змінено для цього контакту!";
            return RedirectToAction("ContactDetails", "ContactDetails", new {EntityId = ContactId});
        }
    }
}
