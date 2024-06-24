using AutoMapper;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Loaders.Contact;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Order;
using CRMEngSystem.Services.Search.Order;
using CRMEngSystem.Models.ViewModels.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Contact
{
    [Authorize]
    public class ContactOrdersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public ContactOrdersController(IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> ContactOrders(ContactOrdersViewModel model)
        {
            var entity = await _repositoryFactory.Instantiate<ContactEntity>().GetEntityAsync(new ContactDataLoader(false, false, false, true, true), entity => entity.ContactId, model.EntityId);
            var searchService = new OrderSearchService(model.OrdersSearchGeneral, model.OrdersSearchGeneral, model.OrdersSearchGeneral);
            return View(new ContactOrdersViewModel
            {
                EntityId = entity!.ContactId,
                Orders = _mapper.Map<IEnumerable<OrderListItemDto>>(searchService.SearchGeneral(entity!.ContactOrders!.Select(contactorder => contactorder.Order))),
                ActiveTab = "Orders",
                NumberOrders = entity!.ContactOrders!.Count,
                NumberComments = entity.Comments!.Count
            });
        }
    }
}
