using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderRemoveController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        public OrderRemoveController(IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager)
        {
            _repositoryFactory = repositoryFactory;
            _userManager = userManager;
        }
        public async Task<IActionResult> OpenModal(int EntityId)
        {
            if ((await _userManager.GetUserAsync(User))!.AccessLevel == AccessLevel.Low)
            {
                TempData["ErrorNotifyModal"] = true;
                TempData["NotifyText"] = "Недостатній рівень доступа для виконання дії.";
                return RedirectToAction("OrderDetails", "OrderDetails", new { EntityId });
            }
            TempData["ConfirmModal"] = true;
            TempData["NotifyText"] = "Ви впевнені що хочете видалити це замовлення?";
            return RedirectToAction("OrderDetails", "OrderDetails", new { EntityId });
        }
        public IActionResult OpenNotifyModal()
        {
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Замовлення видалено успішно.";
            return RedirectToAction("OrderList", "OrderList");
        }
        public async Task<IActionResult> ConfirmModal(int EntityId)
        {
            var repository = _repositoryFactory.Instantiate<OrderEntity>();
            var order = await repository.GetEntityAsync(new OrderDataLoader(true, true, true, true, true), order => order.OrderId, EntityId);
            var result = await repository.RemoveEntityAsync(order!);
            TempData["ConfirmModal"] = false;
            return OpenNotifyModal();
        }
    }
}
