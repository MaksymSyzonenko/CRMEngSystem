using AutoMapper;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Loaders.Contact;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Order;
using CRMEngSystem.Services.Search.Order;
using CRMEngSystem.Models.ViewModels.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CRMEngSystem.Dto.Contact;
using DocumentFormat.OpenXml.Spreadsheet;

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
            var entities = searchService.SearchGeneral(entity!.ContactOrders!.Select(contactorder => contactorder.Order).OrderByDescending(order => order.DateTimeCreate));
            model.TotalPageCount = (int)Math.Ceiling((decimal)entities.Count() / model.NumberItemsPerPage);
            entities = entities.Skip((model.CurrentPage - 1) * model.NumberItemsPerPage).Take(model.NumberItemsPerPage);
            model.Orders = _mapper.Map<IEnumerable<OrderListItemDto>>(entities);
            model.EntityId = entity!.ContactId;
            model.ActiveTab = "Orders";
            model.NumberOrders = entity!.ContactOrders!.Count;
            model.NumberComments = entity.Comments!.Count;
            return View(model);
        }
    }
}
