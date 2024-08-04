using AutoMapper;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Contact;
using CRMEngSystem.Models.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderContactsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public OrderContactsController(IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> OrderContacts(int EntityId)
        {
            var entity = await _repositoryFactory.Instantiate<OrderEntity>().GetEntityAsync(new OrderDataLoader(false, false, true, true, true), entity => entity.OrderId, EntityId);
            var contacts = _mapper.Map<IEnumerable<ContactOrderDto>>(entity!.ContactOrders.Select(contactorder => contactorder.Contact).Where(contact => contact.EnterpriseId != 1));

            var recipientId = entity!.ContactOrders
                .Where(contactorder => contactorder.IsOfferRecipient)
                .Select(contactorder => contactorder.Contact)
                .Where(contact => contact.EnterpriseId != 1)
                .Select(contact => contact.ContactId).FirstOrDefault();

            foreach (var contact in contacts)
            {
                contact.IsOfferRecipient = contact.ContactId == recipientId;
            }
            return View(new OrderContactsViewModel
            {
                Contacts = contacts,
                EntityId = entity!.OrderId,
                ActiveTab = "Contacts",
                NumberEquipmentPositions = entity.EquipmentOrderPositions != null ? entity.EquipmentOrderPositions.Count : 0,
                NumberContacts = entity.ContactOrders != null ? entity.ContactOrders.Where(contactorder => contactorder.Contact.EnterpriseId != 1).Count() : 0,
                NumberWorkGroup = entity.ContactOrders != null ? entity.ContactOrders.Where(contactorder => contactorder.Contact.EnterpriseId == 1).Count() : 0,
                NumberComments = entity.Comments != null ? entity.Comments.Count : 0
            });
        }
    }
}
