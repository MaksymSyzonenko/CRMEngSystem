using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Order;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Models.ViewModels.Order;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Services.Filter.Order;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Services.Search.Order;
using CRMEngSystem.Services.Sort.Order;
using Microsoft.AspNetCore.Authorization;
using CRMEngSystem.Attributes.Cache;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    [ServiceFilter(typeof(ClearCacheAttribute))]
    public class OrderListController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public OrderListController(IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> OrderList(OrderListViewModel model)
        {
            var entities = _repositoryFactory.Instantiate<OrderEntity>().GetAllEntitiesAsQueryable(new OrderDataLoader(true, true, false, false, false));

            model.NumberItemsPerProcessing = entities.Where(entity => entity.Status == OrderStatusValue.Processing).Count();
            model.NumberItemsPerOffer = entities.Where(entity => entity.Status == OrderStatusValue.Offer).Count();
            model.NumberItemsPerExecution = entities.Where(entity => entity.Status == OrderStatusValue.Execution).Count();
            model.NumberItemsPerCompleted = entities.Where(entity => entity.Status == OrderStatusValue.Completed).Count();
            model.NumberItemsPerAll = entities.Count();

            entities = entities.OrderByDescending(entity => entity.DateTimeCreate);

            var filterService = new OrderFilterService(model.FilterStatus, model.FilterPriority, model.FilterDateStart, model.FilterDateEnd);
            entities = filterService.Filter(entities);

            var sortService = new OrderSortService(model.SortOrderId, model.SortAlphabetCustomerName, model.SortPriority, model.SortDateTimeCreate);
            entities = sortService.Sort(entities);

            var searchService = new OrderSearchService(model.SearchOrderId, model.SearchCustomerName, model.SearchInitiatorInitials);
            entities = searchService.Search(entities);

            model.TotalPageCount = (int)Math.Ceiling((decimal)entities.Count() / model.NumberItemsPerPage);
            entities = entities.Skip((model.CurrentPage - 1) * model.NumberItemsPerPage).Take(model.NumberItemsPerPage);

            model.Entities = _mapper.Map<IEnumerable<OrderListItemDto>>(await entities.ToListAsync());
            
            return View(model);
        }
    }
}
