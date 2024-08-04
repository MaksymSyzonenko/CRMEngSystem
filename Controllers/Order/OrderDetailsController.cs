using AutoMapper;
using CRMEngSystem.Attributes.Cache;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Order;
using CRMEngSystem.Models.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    [ServiceFilter(typeof(ClearCacheAttribute))]
    public class OrderDetailsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public OrderDetailsController(IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> OrderDetails(int EntityId)
        {
            var entity = await _repositoryFactory.Instantiate<OrderEntity>().GetEntityAsync(new OrderDataLoader(true, true, true, true, true), entity => entity.OrderId, EntityId);
            return View(new OrderDetailsViewModel
            {
                Order = _mapper.Map<OrderDto>(entity),
                EntityId = entity!.OrderId,
                ActiveTab = "Details",
                NumberEquipmentPositions = entity.EquipmentOrderPositions != null ? entity.EquipmentOrderPositions.Count : 0,
                NumberContacts = entity.ContactOrders != null ? entity.ContactOrders.Where(contactorder => contactorder.Contact.EnterpriseId != 1).Count() : 0,
                NumberWorkGroup = entity.ContactOrders != null ? entity.ContactOrders.Where(contactorder => contactorder.Contact.EnterpriseId == 1).Count() : 0,
                NumberComments = entity.Comments != null ? entity.Comments.Count : 0
            });
        }
    }
}
