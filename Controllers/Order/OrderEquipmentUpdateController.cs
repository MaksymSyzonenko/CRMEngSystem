using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Configuration;
using CRMEngSystem.Services.Calculate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderEquipmentUpdateController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public OrderEquipmentUpdateController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> OrderEquipmentUpdate(int EquipmentOrderPositionId, int Discount, int MarkUp)
        {
            var equipment = await _repositoryFactory.Instantiate<EquipmentOrderPositionEntity>().GetEntityAsync(new EquipmentOrderPositionDataLoader(true, true), equipment => equipment.EquipmentOrderPositionId, EquipmentOrderPositionId);
            equipment.Discount = Discount;
            equipment.MarkUp = MarkUp;
            equipment.PurchasePrice = Math.Round(PriceCalculator.CalculatePurchasePrice(Discount, equipment.BasePrice * ConfigurationSettings.CurrencyCoefficient), 2);
            equipment.SellPrice = Math.Round(PriceCalculator.CalculateSellPrice(Discount, MarkUp, equipment.BasePrice * ConfigurationSettings.CurrencyCoefficient), 2);
            await _repositoryFactory.Instantiate<EquipmentOrderPositionEntity>().UpdateEntityAsync(EquipmentOrderPositionId, equipment);
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Значення знижка та націнки успішно змінено!";
            return RedirectToAction("OrderEquipmentDetails", "OrderEquipmentDetails", new { EntityId = EquipmentOrderPositionId });
        }
    }
}
