using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderChangePriorityController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public OrderChangePriorityController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> OrderChangePriority(int EntityId, PriorityValue Priority)
        {
            var repository = _repositoryFactory.Instantiate<OrderEntity>();
            var order = await repository.GetEntityAsync(new OrderDataLoader(false, false, false, false, false), order => order.OrderId, EntityId);
            order!.Priority = Priority;
            order!.DateTimeUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
            await repository.UpdateEntityAsync(EntityId, order);
            return OpenModal(EntityId);
        }
        public IActionResult OpenModal(int EntityId)
        {
            TempData["ErrorNotifyModal"] = false;
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Пріорітет успішно змінено!";
            return RedirectToAction("OrderDetails", "OrderDetails", new { EntityId });
        }
    }
}
