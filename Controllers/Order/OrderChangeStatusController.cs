using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderChangeStatusController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public OrderChangeStatusController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> OrderChangeStatus(int EntityId, OrderStatusValue Status)
        {
            var repository = _repositoryFactory.Instantiate<OrderEntity>();
            var order = await repository.GetEntityAsync(new OrderDataLoader(false, false, false, false, false), order => order.OrderId, EntityId);
            switch (Status)
            {
                case OrderStatusValue.Processing:
                    order!.DateTimeProcessingStatusStart = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
                    break;
                case OrderStatusValue.Offer:
                    order!.DateTimeOfferStatusStart = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
                    break;
                case OrderStatusValue.Execution:
                    order!.DateTimeExecutionStatusStart = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
                    break;
                case OrderStatusValue.Completed:
                    order!.DateTimeCompletedStatusStart = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
                    break;
            }
            order!.Status = Status;
            order!.DateTimeUpdate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"));
            await repository.UpdateEntityAsync(EntityId, order);
            return OpenModal(EntityId);
        }
        public IActionResult OpenModal(int EntityId)
        {
            TempData["ErrorNotifyModal"] = false;
            TempData["NotifyModal"] = true;
            TempData["NotifyText"] = "Статус успішно змінено!";
            return RedirectToAction("OrderDetails", "OrderDetails", new { EntityId });
        }
    }
}
