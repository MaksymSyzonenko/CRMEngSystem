using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    public class OrderChangeOfferRecipientController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public OrderChangeOfferRecipientController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<IActionResult> OrderChangeOfferRecipient(int OrderId, int ContactId)
        {
            var order = await _repositoryFactory.Instantiate<OrderEntity>()
                .GetEntityAsync(new OrderDataLoader(false, false, true, false, false), order => order.OrderId, OrderId);
            foreach(var contactorder in order.ContactOrders)
            {
                contactorder.IsOfferRecipient = contactorder.ContactId == ContactId;
            }
            await _repositoryFactory.Instantiate<ContactOrderEntity>().UpdateRangeAsync(order.ContactOrders);
            return RedirectToAction("OrderContacts", "OrderContacts", new { EntityId = OrderId });
        }
    }
}
