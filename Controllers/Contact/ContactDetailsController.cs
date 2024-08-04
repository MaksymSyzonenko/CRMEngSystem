using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Loaders.Contact;
using CRMEngSystem.Dto.Contact;
using CRMEngSystem.Models.ViewModels.Contact;
using Microsoft.AspNetCore.Authorization;
using CRMEngSystem.Attributes.Cache;

namespace CRMEngSystem.Controllers.Contact
{
    [Authorize]
    [ServiceFilter(typeof(ClearCacheAttribute))]
    public class ContactDetailsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public ContactDetailsController(IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> ContactDetails(int EntityId)
        {
            var entity = await _repositoryFactory.Instantiate<ContactEntity>().GetEntityAsync(new ContactDataLoader(true, true, true, true, true), entity => entity.ContactId, EntityId);
            return View(new ContactDetailsViewModel
            {
                Contact = _mapper.Map<ContactDto>(entity),
                EntityId = entity!.ContactId,
                ActiveTab = "Details",
                NumberOrders = entity.ContactOrders != null ? entity.ContactOrders.Count : 0,
                NumberComments = entity.Comments != null ? entity.Comments.Count : 0
            });
        }
    }
}
