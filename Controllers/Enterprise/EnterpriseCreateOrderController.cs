using AutoMapper;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Mvc;
using CRMEngSystem.Data.Enums;
using Microsoft.AspNetCore.Identity;
using CRMEngSystem.Data.Entities.User;
using Microsoft.AspNetCore.Authorization;

namespace CRMEngSystem.Controllers.Enterprise
{
    [Authorize]
    public class EnterpriseCreateOrderController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        public EnterpriseCreateOrderController(IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager)
        {
            _repositoryFactory = repositoryFactory;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> EnterpriseCreateOrder(int EntityId)
        {
            var contactId = (await _userManager.GetUserAsync(User))!.ContactId;
            var result = await _repositoryFactory.Instantiate<OrderEntity>().AddEntityAsync(new OrderEntity
            {
                CustomerId = EntityId,
                Status = OrderStatusValue.Processing,
                DateTimeProcessingStatusStart = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time")),
                Priority = PriorityValue.Low,
                DateTimeCreate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time")),
                DateTimeUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time")),
                InitiatorId = contactId,
                ContactOrders = new List<ContactOrderEntity>()
                {
                    new()
                    {
                        ContactId = contactId
                    }
                }
            });
            if (result)
                return OpenModal();
            return OpenErrorModal();
        }
        public IActionResult OpenModal()
        {
            TempData["ErrorNotifyModal"] = false;
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Замовлення створено успішно!";
            return RedirectToAction("OrderList", "OrderList");
        }
        public IActionResult OpenErrorModal()
        {
            TempData["ErrorNotifyModal"] = true;
            TempData["NotifyModal"] = false;
            return RedirectToAction("OrderList", "OrderList");
        }
    }
}
