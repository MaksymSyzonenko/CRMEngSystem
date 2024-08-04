using CRMEngSystem.Configuration;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Services.Calculate;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    public class OrderTotalPriceValuesUpdateController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public OrderTotalPriceValuesUpdateController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> OrderTotalDiscountUpdate(int Discount, int OrderId)
        {
            var repository = _repositoryFactory.Instantiate<OrderEntity>();
            var order = await repository.GetEntityAsync(new OrderDataLoader(false, false, false, false, true), order => order.OrderId, OrderId);
            if(order!.EquipmentOrderPositions != null)
            {
                order.EquipmentOrderPositions.ToList().ForEach(position =>
                {
                    position.Discount = Discount;
                    position.PurchasePrice = Math.Round(PriceCalculator.CalculatePurchasePrice(position.Discount, position.EquipmentCatalogPosition.BasePrice * ConfigurationSettings.CurrencyCoefficient), 2);
                    position.SellPrice = Math.Round(PriceCalculator.CalculateSellPrice(position.Discount, position.MarkUp, position.EquipmentCatalogPosition.BasePrice * ConfigurationSettings.CurrencyCoefficient), 2);
                });
                await repository.UpdateEntityAsync(OrderId, order);
            }
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Значення знижки для всіх позицій змінено";
            return RedirectToAction("OrderEquipment", "OrderEquipment", new { EntityId = OrderId });
        }
        public async Task<IActionResult> OrderTotalMarkUpUpdate(int MarkUp, int OrderId)
        {
            var repository = _repositoryFactory.Instantiate<OrderEntity>();
            var order = await repository.GetEntityAsync(new OrderDataLoader(false, false, false, false, true), order => order.OrderId, OrderId);
            if (order!.EquipmentOrderPositions != null)
            {
                order.EquipmentOrderPositions.ToList().ForEach(position =>
                {
                    position.MarkUp = MarkUp;
                    position.PurchasePrice = Math.Round(PriceCalculator.CalculatePurchasePrice(position.Discount, position.EquipmentCatalogPosition.BasePrice * ConfigurationSettings.CurrencyCoefficient), 2);
                    position.SellPrice = Math.Round(PriceCalculator.CalculateSellPrice(position.Discount, position.MarkUp, position.EquipmentCatalogPosition.BasePrice * ConfigurationSettings.CurrencyCoefficient), 2);
                });
                await repository.UpdateEntityAsync(OrderId, order);
            }
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Значення націнки для всіх позицій змінено";
            return RedirectToAction("OrderEquipment", "OrderEquipment", new { EntityId = OrderId });
        }
    }
}
