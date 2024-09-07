using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Configuration;
using CRMEngSystem.Services.Calculate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderUpdatePricesController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        public OrderUpdatePricesController(IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager)
        {
            _repositoryFactory = repositoryFactory;
            _userManager = userManager;
        }
        public async Task<IActionResult> OpenModal(int EntityId)
        {
            if((await _userManager.GetUserAsync(User))!.AccessLevel != AccessLevel.Low)
            {
                TempData["ConfirmModalUpdate"] = true;
                TempData["NotifyText"] = "Ви впевнені що хочете оновити значення прайсів, цін та вартостей на актуальні?";
                return RedirectToAction("OrderDetails", "OrderDetails", new { EntityId });
            }
            TempData["ErrorNotifyModal"] = true;
            TempData["NotifyText"] = "Недостатній рівень доступу для оновлення значення прайсів, цін та вартостей на актуальні.";
            return RedirectToAction("OrderDetails", "OrderDetails", new { EntityId });
        }
        public async Task<IActionResult> ConfirmModal(int EntityId)
        {
            var repository1 = _repositoryFactory.Instantiate<OrderEntity>();
            var repository2 = _repositoryFactory.Instantiate<EquipmentOrderPositionEntity>();
            var order = await repository1.GetEntityAsync(new OrderDataLoader(true, true, true, true, true), order => order.OrderId, EntityId);
            var equipmentList = repository2.GetAllEntitiesAsQueryable(new EquipmentOrderPositionDataLoader(true, true))
                    .Where(equipment => equipment.OrderId == EntityId);
            var updatedEntities = new List<EquipmentOrderPositionEntity>();
            var entityIds = new List<int>();
            foreach (var equipment in equipmentList)
            {
                equipment.BasePrice = equipment.EquipmentCatalogPosition.BasePrice;
                equipment.PurchasePrice = Math.Round(PriceCalculator.CalculatePurchasePrice(equipment.Discount, equipment.EquipmentCatalogPosition.BasePrice * ConfigurationSettings.CurrencyCoefficient), 2);
                equipment.SellPrice = Math.Round(PriceCalculator.CalculateSellPrice(equipment.Discount, equipment.MarkUp, equipment.EquipmentCatalogPosition.BasePrice * ConfigurationSettings.CurrencyCoefficient), 2);
                equipment.ShippingCost = Math.Round(PriceCalculator.CalculateShippingCost(equipment.Weight, equipment.Volume, ConfigurationSettings.ShippingRatePerKg, ConfigurationSettings.ShippingRatePerCubicMeter), 2);
                entityIds.Add(equipment.EquipmentOrderPositionId);
                updatedEntities.Add(equipment);
            }
            await repository2.UpdateRangeAsync("EquipmentOrderPositionId", entityIds, updatedEntities);
            order.DateTimeUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
            await repository1.UpdateEntityAsync(EntityId, order);
            TempData["NotifyModal"] = true;
            TempData["ConfirmModalUpdate"] = false;
            TempData["ErrorNotifyModal"] = false;
            TempData["NotifyText"] = "Значення оновлено на актуальні!";
            return RedirectToAction("OrderDetails", "OrderDetails", new { EntityId });
        }
    }
}
