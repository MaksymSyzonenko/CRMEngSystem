using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Excel;
using CRMEngSystem.Word;
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
        public async Task<IActionResult> OrderCreateOrder(int OrderId)
        {
            if ((await _userManager.GetUserAsync(User))!.AccessLevel != AccessLevel.High)
            {
                TempData["ErrorNotifyModal"] = true;
                TempData["NotifyText"] = "Недостатній рівень доступа для виконання дії.";
                return RedirectToAction("OrderDetails", "OrderDetails", new { EntityId = OrderId });
            }
            var order = await _repositoryFactory.Instantiate<OrderEntity>().GetEntityAsync(new OrderDataLoader(true, false, false, false, true), order => order.OrderId, OrderId);
            string templatePath = "OrderPattern.xlsx";
            string outputPath = Guid.NewGuid() + ".xlsx";
            System.IO.File.Copy(templatePath, outputPath, true);
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
                    TradeGroup = "-",
                    OrderNumber = OrderId.ToString()
                });
            }
            CommercialOrderCreater commercialOrderCreater = new(outputPath, equipmentList, "LastName FirstName");
            commercialOrderCreater.CreateCommercialOrder();
            byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);
            string fileName = $"Комерційне_Замовлення_{OrderId}.xlsx";
            System.IO.File.Delete(outputPath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }
    }
}
