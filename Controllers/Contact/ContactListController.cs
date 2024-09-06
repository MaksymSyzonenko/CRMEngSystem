using AutoMapper;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Loaders.Contact;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Contact;
using CRMEngSystem.Services.Search.Contact;
using CRMEngSystem.Models.ViewModels.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Attributes.Cache;

namespace CRMEngSystem.Controllers.Contact
{
    [Authorize]
    [ServiceFilter(typeof(ClearCacheAttribute))]
    public class ContactListController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public ContactListController(IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }

        [HttpGet]
        public IActionResult ContactList(ContactListViewModel model)
        {
            var entities = _repositoryFactory.Instantiate<ContactEntity>().GetAllEntitiesAsQueryable(new ContactDataLoader(true, true, true, false, false));

            entities = entities.OrderByDescending(entity => entity.DateTimeCreate);

            var searchService = new ContactSearchService(model.SearchGeneral);
            entities = searchService.Search(entities);

            model.TotalPageCount = (int)Math.Ceiling((decimal)entities.Count() / model.NumberItemsPerPage);
            entities = entities.Skip((model.CurrentPage - 1) * model.NumberItemsPerPage).Take(model.NumberItemsPerPage);
            model.Entities = _mapper.Map<IEnumerable<ContactListItemDto>>(entities.ToList());

            return View(model);
        }
    }
}
