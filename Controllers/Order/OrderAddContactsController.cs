using AutoMapper;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Loaders.Enterprise;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Contact;
using CRMEngSystem.Models.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{

    [Authorize]
    public class OrderAddContactsController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        public OrderAddContactsController(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> OrderAddContacts(int OrderId, bool WorkGroup)
        {
            var order = await _repositoryFactory.Instantiate<OrderEntity>().GetEntityAsync(new OrderDataLoader(true, false, true, false, false), order => order.OrderId, OrderId);
            int enterpriseId = 0;
            if (WorkGroup)
            {
                enterpriseId = 1;
            }
            else
            {
                enterpriseId = order.Customer.EnterpriseId;
            }
            var enterprise =  await _repositoryFactory.Instantiate<EnterpriseEntity>().GetEntityAsync(new EnterpriseDataLoader(true, false, true, false), enterprise => enterprise.EnterpriseId, enterpriseId);
            var orderContacts = order.ContactOrders != null ? order.ContactOrders.Select(contactorder => contactorder.Contact) : null;
            var contacts = enterprise.Contacts.AsEnumerable();
            if (orderContacts != null) 
            {
                contacts = contacts.Except(orderContacts);
            }
            return View(new OrderAddContactsViewModel
            {
                WorkGroup = WorkGroup,
                OrderId = OrderId,
                Contacts = _mapper.Map<List<ContactSelectDto>>(contacts)
            });
        }
        [HttpPost]
        public async Task<IActionResult> OrderAddContacts(OrderAddContactsViewModel model)
        {
            List<ContactSelectDto> selectedContacts = model.Contacts.Where(contact => contact.IsSelected).ToList();
            var order = await _repositoryFactory.Instantiate<OrderEntity>()
                .GetEntityAsync(new OrderDataLoader(false, false, true, false, false), order => order.OrderId, model.OrderId);
            var existingContactIds = order.ContactOrders.Select(contact => contact.Contact.ContactId).ToHashSet();
            selectedContacts.RemoveAll(contact => existingContactIds.Contains(contact.ContactId));

            bool isOfferRecipient = !order.ContactOrders.Select(contactorder => contactorder.Contact).Any(contact => contact.EnterpriseId != 1);

            foreach (var contact in selectedContacts)
            {
                await _repositoryFactory.Instantiate<ContactOrderEntity>().AddEntityAsync(new ContactOrderEntity
                {
                    ContactId = contact.ContactId,
                    OrderId = model.OrderId,
                    IsOfferRecipient = isOfferRecipient
                });
            }
            if(model.WorkGroup)
                return RedirectToAction("OrderWorkGroup", "OrderWorkGroup", new { EntityId = model.OrderId });
            return RedirectToAction("OrderContacts", "OrderContacts", new { EntityId = model.OrderId });
        }
    }
}
