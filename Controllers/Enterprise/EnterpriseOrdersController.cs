using AutoMapper;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Loaders.Enterprise;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Enterprise;
using CRMEngSystem.Services.Filter.Order;
using CRMEngSystem.Models.ViewModels.Enterprise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Enterprise
{
    [Authorize]
    public class EnterpriseOrdersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public EnterpriseOrdersController(IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> EnterpriseOrders(EnterpriseOrdersViewModel model)
        {
            var entity = await _repositoryFactory.Instantiate<EnterpriseEntity>().GetEntityAsync(new EnterpriseDataLoader(false, true, true, true), entity => entity.EnterpriseId, model.EntityId);

            var entities = entity!.Orders!.OrderByDescending(order => order.DateTimeCreate).AsQueryable();

            if (!string.IsNullOrEmpty(model.SearchOrderId))
                entities = entities.Where(entity => entity.OrderId.ToString() == model.SearchOrderId);

            if (model.FilterPriority != null && OrderFilterMap.PriorityMap.TryGetValue(model.FilterPriority, out var priority))
                entities = entities.Where(entity => entity.Priority == priority);

            if (!string.IsNullOrEmpty(model.SearchInitiatorInitials))
                entities = entities.Where(entity => entity.Initiator.Details.FirstName.ToLower().Contains(model.SearchInitiatorInitials.ToLower()) ||
                     entity.Initiator.Details.LastName.ToLower().Contains(model.SearchInitiatorInitials.ToLower()) ||
                     entity.Initiator.Details.MiddleName.ToLower().Contains(model.SearchInitiatorInitials.ToLower()) ||
                     model.SearchInitiatorInitials.ToLower() == (entity.Initiator.Details.LastName.Substring(0, 1).ToLower() + entity.Initiator.Details.FirstName.Substring(0, 1).ToLower() + entity.Initiator.Details.MiddleName.Substring(0, 1).ToLower()));

            if (model.FilterMinSellPrice != null)
                entities = entities.Where(entity => entity.EquipmentOrderPositions!.Sum(position => position.SellPrice * position.Quantity) >= model.FilterMinSellPrice);

            if (model.FilterMaxSellPrice != null)
                entities = entities.Where(entity => entity.EquipmentOrderPositions!.Sum(position => position.SellPrice * position.Quantity) <= model.FilterMaxSellPrice);

            if (model.FilterDateStart.HasValue)
                entities = entities.Where(entity => DateOnly.FromDateTime(entity.DateTimeCreate) >= model.FilterDateStart.Value);

            if (model.FilterDateEnd.HasValue)
            {
                var endDate = model.FilterDateEnd.Value.AddDays(1);
                entities = entities.Where(entity => DateOnly.FromDateTime(entity.DateTimeCreate) < endDate);
            }

            if (model.FilterStatus != null && OrderFilterMap.StatusMap.TryGetValue(model.FilterStatus, out var status))
                entities = entities.Where(entity => entity.Status == status);

            model.TotalPageCount = (int)Math.Ceiling((decimal)entities.Count() / model.NumberItemsPerPage);
            entities = entities.Skip((model.CurrentPage - 1) * model.NumberItemsPerPage).Take(model.NumberItemsPerPage);

            return View(new EnterpriseOrdersViewModel
            {
                EntityId = entity!.EnterpriseId,
                Orders = _mapper.Map<IEnumerable<EnterpriseOrderDto>>(entities),
                ActiveTab = "Orders",
                NumberOrders = entity!.Orders!.Count,
                NumberContacts = entity!.Contacts!.Count,
                NumberComments = entity.Comments!.Count,
                CurrentPage = model.CurrentPage,
                TotalPageCount = model.TotalPageCount,
                NumberItemsPerPage = model.NumberItemsPerPage
            });
        }
    }
}
