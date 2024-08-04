using AutoMapper;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Order;
using CRMEngSystem.Models.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderEquipmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        public OrderEquipmentController(IMapper mapper, IRepositoryFactory repositoryFactory)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
        }
        [HttpGet]
        public async Task<IActionResult> OrderEquipment(int EntityId)
        {
            var entity = await _repositoryFactory.Instantiate<OrderEntity>().GetEntityAsync(new OrderDataLoader(false, false, true, true, true), entity => entity.OrderId, EntityId);
            var equipmentPositions = _mapper.Map<IEnumerable<OrderEquipmentPositionDto>>(entity!.EquipmentOrderPositions);
            var positions = _repositoryFactory
                .Instantiate<EquipmentWareHouseOrderEntity>()
                .GetAllEntitiesAsQueryable(new EquipmentWareHouseOrderDataLoader(false, false));
            var equipmentWareHousePositions = _repositoryFactory
                .Instantiate<EquipmentWareHousePositionEntity>()
                .GetAllEntitiesAsQueryable(new EquipmentWareHousePositionDataLoader(false, false));
            foreach (var equipment in equipmentPositions)
            {
                int quantityToTake = positions.Where(position => position.EquipmentOrderPositionId == equipment.EquipmentOrderPositionId).Sum(arg => arg.Quantity);
                equipment.QuantityToTake = quantityToTake;
                equipment.QuantityInStock = quantityToTake + equipmentWareHousePositions.Where(equip => equip.EquipmentCatalogPositionId == equipment.EquipmentCatalogPositionId).Sum(arg => arg.Quantity);
            }
            return View(new OrderEquipmentViewModel
            {
                EquipmentPositions = equipmentPositions,
                EntityId = entity!.OrderId,
                ActiveTab = "EquipmentPositions",
                NumberEquipmentPositions = entity.EquipmentOrderPositions != null ? entity.EquipmentOrderPositions.Count : 0,
                NumberContacts = entity.ContactOrders != null ? entity.ContactOrders.Where(contactorder => contactorder.Contact.EnterpriseId != 1).Count() : 0,
                NumberWorkGroup = entity.ContactOrders != null ? entity.ContactOrders.Where(contactorder => contactorder.Contact.EnterpriseId == 1).Count() : 0,
                NumberComments = entity.Comments != null ? entity.Comments.Count : 0
            });
        }
    }
}
