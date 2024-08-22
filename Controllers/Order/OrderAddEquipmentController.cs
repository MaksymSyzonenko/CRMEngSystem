using CRMEngSystem.Data.Entities.Catalog;
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
        public OrderAddEquipmentController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> OrderAddEquipment(
            int OrderId, 
            int EquipmentCatalogPositionId, 
            string? SearchGeneral,
            string? SearchEquipmentCode,
            string? SearchName,
            string? FilterType,
            decimal? FilterMinBasePrice,
            decimal? FilterMaxBasePrice,
            decimal? FilterMinWeight,
            decimal? FilterMaxWeight,
            decimal? FilterMinVolume,
            decimal? FilterMaxVolume,
            bool? SortCode,
            bool? SortAlphabetNameEN,
            bool? SortPrice,
            bool? SortWeight,
            bool? SortVolume,
            int CurrentPage,
            int Quantity
        )
        {
            var order = await _repositoryFactory.Instantiate<OrderEntity>().GetEntityAsync(new OrderDataLoader(false, false, false, false, true), order => order.OrderId, OrderId);
            if(order.EquipmentOrderPositions.Any(equipment => equipment.EquipmentCatalogPositionId == EquipmentCatalogPositionId))
            {
                var equipment = order.EquipmentOrderPositions.FirstOrDefault(equipment => equipment.EquipmentCatalogPositionId == EquipmentCatalogPositionId);
                equipment.Quantity = Quantity;
                await _repositoryFactory
                    .Instantiate<EquipmentOrderPositionEntity>()
                    .UpdateEntityAsync(equipment.EquipmentOrderPositionId, equipment);
                TempData["NotifyModal"] = true;
                TempData["NotifyText"] = "Обладнання успішно замінено для замовлення!";
            }
            else
            {
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
                    Quantity = Quantity,
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
            }
            
            return RedirectToAction("CatalogList", "CatalogList", new CatalogListViewModel { 
                OrderId = OrderId,
                SearchGeneral = SearchGeneral,
                SearchEquipmentCode = SearchEquipmentCode,
                SearchName = SearchName,
                FilterType = FilterType,
                FilterMinBasePrice = FilterMinBasePrice,
                FilterMaxBasePrice = FilterMaxBasePrice,
                FilterMinWeight = FilterMinWeight,
                FilterMaxWeight = FilterMaxWeight,
                FilterMinVolume = FilterMinVolume,
                FilterMaxVolume = FilterMaxVolume,
                SortCode = SortCode,
                SortAlphabetNameEN = SortAlphabetNameEN,
                SortPrice = SortPrice,
                SortWeight = SortWeight,
                SortVolume = SortVolume,
                CurrentPage = CurrentPage
            });
        }
    }
}
