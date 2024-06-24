using AutoMapper;
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
            var equipment = await _repositoryFactory.Instantiate<EquipmentOrderPositionEntity>().GetEntityAsync(new EquipmentOrderPositionDataLoader(false, true), equipment => equipment.EquipmentOrderPositionId, EntityId);
            return View(new OrderEquipmentDetailsViewModel
            {
                OrderPosition = _mapper.Map<OrderEquipmentPositionDetailsDto>(equipment)
            });
        }
        public IActionResult CloseModal(int EntityId)
        {
            TempData["NotifyModal"] = false;
            TempData["ErorrNotifyModal"] = false;
            return RedirectToAction("OrderEquipmentDetails", "OrderEquipmentDetails", new { EntityId });
        }
    }
}
