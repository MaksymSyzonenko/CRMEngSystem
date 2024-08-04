using AutoMapper;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Order;
using CRMEngSystem.Dto.WareHouse;
using CRMEngSystem.Models.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderEquipmentDetailsController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        public OrderEquipmentDetailsController(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }
        public async Task<IActionResult> OrderEquipmentDetails(int EntityId)
        {
            var equipment = await _repositoryFactory
                .Instantiate<EquipmentOrderPositionEntity>()
                .GetEntityAsync(new EquipmentOrderPositionDataLoader(false, true), equipment => equipment.EquipmentOrderPositionId, EntityId);

            var wareHouses = await _repositoryFactory
                .Instantiate<WareHouseEntity>()
                .GetAllEntitiesAsQueryable(new WareHouseDataLoader(true))
                .ToListAsync();

            var equipmentWareHouseOrderListDto = new List<EquipmentWareHouseOrderDto>();
            var equipmentWareHouseOrderList = await _repositoryFactory
                .Instantiate<EquipmentWareHouseOrderEntity>()
                .GetAllEntitiesAsQueryable(new EquipmentWareHouseOrderDataLoader(true, true))
                .ToListAsync();

            int quantityWareHouse = 0;

            foreach (var wareHouse in wareHouses)
            {
                var currentEquipment = wareHouse.EquipmentWareHousePositions
                    .FirstOrDefault(equip => equip.EquipmentCatalogPositionId == equipment.EquipmentCatalogPositionId);

                int quantityInStock = currentEquipment != null ? currentEquipment.Quantity : 0;

                var position = equipmentWareHouseOrderList
                    .FirstOrDefault(position => position.EquipmentOrderPositionId == EntityId && position.WareHouseId == wareHouse.WareHouseId);

                equipmentWareHouseOrderListDto.Add(new EquipmentWareHouseOrderDto()
                {
                    WareHouseId = wareHouse.WareHouseId,
                    WareHouseName = wareHouse.Name,
                    EquipmentOrderPositionId = EntityId,
                    EquipmentCatalogPositionId = equipment!.EquipmentCatalogPositionId,
                    Quantity = position?.Quantity ?? 0,
                    QuantityInStock = quantityInStock + (position?.Quantity ?? 0)
                });

                quantityWareHouse += position?.Quantity ?? 0;
            }

            return View(new OrderEquipmentDetailsViewModel
            {
                OrderPosition = _mapper.Map<OrderEquipmentPositionDetailsDto>(equipment),
                EquipmentWareHouseOrderList = equipmentWareHouseOrderListDto,
                QuantityWareHouse = quantityWareHouse
            });
        }

    }
}
