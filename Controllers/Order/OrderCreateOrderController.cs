using CRMEngSystem.Configuration;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Loaders.WareHouse;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderCreateOrderController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        public OrderCreateOrderController(IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager)
        {
            _repositoryFactory = repositoryFactory;
            _userManager = userManager;
        }
        public async Task<IActionResult> OpenModal(int OrderId)
        {
            if ((await _userManager.GetUserAsync(User))!.AccessLevel != AccessLevel.High)
            {
                TempData["ErrorNotifyModal"] = true;
                TempData["NotifyText"] = "Недостатній рівень доступа для виконання дії.";
                return RedirectToAction("OrderDetails", "OrderDetails", new { EntityId = OrderId });
            }
            TempData["OrderWareHouseConfirmModal"] = true;
            return RedirectToAction("OrderDetails", "OrderDetails", new { EntityId = OrderId });
        }
        public async Task<IActionResult> OrderCreateOrder(int OrderId, bool IncludeWareHouse)
        {
            if (IncludeWareHouse)
            {
                var repository = _repositoryFactory.Instantiate<EquipmentWareHouseOrderEntity>();
                var order = await _repositoryFactory.Instantiate<OrderEntity>().GetEntityAsync(new OrderDataLoader(true, false, false, false, true), order => order.OrderId, OrderId);
                var equipmentList = new List<OrderEntry>();
                foreach (var equipment in order.EquipmentOrderPositions)
                {
                    int quantity = repository
                        .GetAllEntitiesAsQueryable(new EquipmentWareHouseOrderDataLoader(true, false))
                        .Where(equip => equip.EquipmentOrderPositionId == equipment.EquipmentOrderPositionId)
                        .Sum(equip => equip.Quantity);
                    int result = equipment.Quantity - quantity;
                    if(result > 0)
                    {
                        equipmentList.Add(new OrderEntry
                        {
                            PartNumber = equipment.EquipmentCatalogPosition.EquipmentCode,
                            Description = equipment.EquipmentCatalogPosition.NameEN,
                            Customer = order.Customer.Details.NameEN,
                            Quantity = result,
                            Price = equipment.PurchasePrice,
                            TradeGroup = order.Customer.Details.TradeGroup,
                            OrderNumber = OrderId.ToString()
                        });
                    }
                }
                byte[] fileBytes = Convert.FromBase64String(ConfigurationSettings.ExcelTemplate);
                CommercialOrderCreater commercialOrderCreater = new(fileBytes, equipmentList, "Sergei Sizonenko");
                fileBytes = commercialOrderCreater.CreateCommercialOrder();
                string fileName = $"Комерційне_Замовлення_{OrderId}.xlsx";
                TempData["OrderWareHouseConfirmModal"] = false;
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
            }
            else
            {
                var order = await _repositoryFactory.Instantiate<OrderEntity>().GetEntityAsync(new OrderDataLoader(true, false, false, false, true), order => order.OrderId, OrderId);
                var equipmentList = new List<OrderEntry>();
                foreach (var equipment in order.EquipmentOrderPositions)
                {
                    equipmentList.Add(new OrderEntry
                    {
                        PartNumber = equipment.EquipmentCatalogPosition.EquipmentCode,
                        Description = equipment.EquipmentCatalogPosition.NameEN,
                        Customer = order.Customer.Details.NameEN,
                        Quantity = equipment.Quantity,
                        Price = equipment.PurchasePrice,
                        TradeGroup = order.Customer.Details.TradeGroup,
                        OrderNumber = OrderId.ToString()
                    });
                }
                byte[] fileBytes = Convert.FromBase64String(ConfigurationSettings.ExcelTemplate);
                CommercialOrderCreater commercialOrderCreater = new(fileBytes, equipmentList, "Sergei Sizonenko");
                fileBytes = commercialOrderCreater.CreateCommercialOrder();
                string fileName = $"Комерційне_Замовлення_{OrderId}.xlsx";
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
            }
        }
    }
}
