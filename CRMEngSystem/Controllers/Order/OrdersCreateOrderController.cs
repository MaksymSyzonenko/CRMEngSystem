using AutoMapper;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Contact;
using CRMEngSystem.Dto.Order;
using CRMEngSystem.Excel;
using CRMEngSystem.Models.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrdersCreateOrderController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        public OrdersCreateOrderController(IRepositoryFactory repositoryFactory, IMapper mapper, UserManager<UserEntity> userManager)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> OrdersCreateOrder()
        {
            if ((await _userManager.GetUserAsync(User))!.AccessLevel != AccessLevel.High)
            {
                TempData["ErrorNotifyModal"] = true;
                TempData["NotifyText"] = "Недостатній рівень доступа для виконання дії.";
                return RedirectToAction("OrderList", "OrderList");
            }
            var orders = _repositoryFactory.Instantiate<OrderEntity>().GetAllEntitiesAsQueryable(new OrderDataLoader(true, false, false, false, false)).Where(order => order.Status == CRMEngSystem.Data.Enums.OrderStatusValue.Execution);
            return View(new OrdersCreateOrderViewModel
            {
                Orders = _mapper.Map<List<OrdersCreateOrderDto>>(orders)
            });
        }
        [HttpPost]
        public async Task<IActionResult> OrdersCreateOrder(OrdersCreateOrderViewModel model)
        {
            List<OrdersCreateOrderDto> selectedOrders = model.Orders.Where(order => order.IsSelected).ToList();
            if (selectedOrders.Count == 0)
            {
                return await OrdersCreateOrder();
            }
            string templatePath = "OrderPattern.xlsx";
            string outputPath = Guid.NewGuid() + ".xlsx";
            System.IO.File.Copy(templatePath, outputPath, true);
            var equipmentList = new List<OrderEntry>();
            foreach (var orderDto in selectedOrders)
            {
                var order = await _repositoryFactory.Instantiate<OrderEntity>().GetEntityAsync(new OrderDataLoader(true, false, false, false, true), order => order.OrderId, orderDto.OrderId);
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
                        OrderNumber = order.OrderId.ToString()
                    });
                }
            }
            CommercialOrderCreater commercialOrderCreater = new(outputPath, equipmentList, "Sergei Sizonenko");
            commercialOrderCreater.CreateCommercialOrder();
            byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);
            string fileName = $"Комерційне_Замовлення.xlsx";
            System.IO.File.Delete(outputPath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }
    }
}
