using AutoMapper;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Loaders.Enterprise;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Contact;
using CRMEngSystem.Models.ViewModels.Enterprise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Enterprise
{
    [Authorize]
    public class EnterpriseContactsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public EnterpriseContactsController(IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> EnterpriseContacts(int EntityId)
        {
            var entity = await _repositoryFactory.Instantiate<EnterpriseEntity>().GetEntityAsync(new EnterpriseDataLoader(false, true, true, true), entity => entity.EnterpriseId, EntityId);
            return View(new EnterpriseContactsViewModel
            {
                Contacts = _mapper.Map<IEnumerable<ContactListItemDto>>(entity!.Contacts!.OrderByDescending(entity => entity.DateTimeCreate)),
                EntityId = entity!.EnterpriseId,
                ActiveTab = "Contacts",
                NumberOrders = entity.Orders != null ? entity.Orders.Count : 0,
                NumberContacts = entity.Contacts != null ? entity.Contacts.Count : 0,
                NumberComments = entity.Comments != null ? entity.Comments.Count : 0
            });
        }
    }
}
