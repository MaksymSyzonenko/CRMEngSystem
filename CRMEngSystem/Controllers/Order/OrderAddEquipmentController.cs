﻿using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Loaders.Catalog;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Configuration;
using CRMEngSystem.Services.Calculate;
using CRMEngSystem.Models.ViewModels.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderAddEquipmentController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IConfiguration _configuration;
        public OrderAddEquipmentController(IRepositoryFactory repositoryFactory, IConfiguration configuration)
        {
            _repositoryFactory = repositoryFactory;
            _configuration = configuration;

        }
        public async Task<IActionResult> OrderAddEquipment(int OrderId, int EquipmentCatalogPositionId)
        {
            var order = await _repositoryFactory.Instantiate<OrderEntity>().GetEntityAsync(new OrderDataLoader(false, false, false, false, true), order => order.OrderId, OrderId);
            if(order.EquipmentOrderPositions.Any(equipment => equipment.EquipmentCatalogPositionId == EquipmentCatalogPositionId))
            {
                return RedirectToAction("CatalogList", "CatalogList", new CatalogListViewModel { OrderId = OrderId }); ;
            }

            var equipment = await _repositoryFactory.Instantiate<EquipmentCatalogPositionEntity>().GetEntityAsync(new EquipmentCatalogPositionDataLoader(false, false, false), equipment => equipment.EquipmentCatalogPositionId, EquipmentCatalogPositionId);
            int discount = 35;
            int markUp = 55;
            await _repositoryFactory.Instantiate<EquipmentOrderPositionEntity>().AddEntityAsync(new EquipmentOrderPositionEntity
            {
                OrderId = OrderId,
                EquipmentCatalogPositionId = EquipmentCatalogPositionId,
                BasePrice = equipment.BasePrice,
                Discount = discount,
                MarkUp = markUp,
                Quantity = 1,
                PurchasePrice = Math.Round(PriceCalculator.CalculatePurchasePrice(discount, equipment.BasePrice * ConfigurationSettings.CurrencyCoefficient), 2),
                SellPrice = Math.Round(PriceCalculator.CalculateSellPrice(discount, markUp, equipment.BasePrice * ConfigurationSettings.CurrencyCoefficient), 2),
                Weight = equipment.Weight,
                Volume = equipment.Volume,
                QuantityInStock = 1,
                QuantityToTake = 1,
                ShippingCost = Math.Round(PriceCalculator.CalculateShippingCost(equipment.Weight, equipment.Volume, ConfigurationSettings.ShippingRatePerKg, ConfigurationSettings.ShippingRatePerCubicMeter), 2)
            });
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Обладнання успішно додано до замовлення!";
            return RedirectToAction("CatalogList", "CatalogList", new CatalogListViewModel { OrderId = OrderId }); ;
        }
    }
}
