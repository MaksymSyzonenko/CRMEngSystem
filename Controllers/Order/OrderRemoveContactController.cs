using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderRemoveContactController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        public OrderRemoveContactController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public IActionResult OrderRemoveContact(int OrderId, string ActionName, string ControllerName, int ContactId)
        {
            TempData["ConfirmModal"] = true;
            TempData["ContactId"] = ContactId;
            return RedirectToAction(ActionName, ControllerName, new { EntityId = OrderId });
        }
        public async Task<IActionResult> ConfirmModal(int OrderId, int ContactId, string ActionName, string ControllerName)
        {
            var contactOrders = _repositoryFactory.Instantiate<ContactOrderEntity>().GetAllEntitiesAsQueryable(new ContactOrderDataLoader(true, true));
            var contactOrder = contactOrders.FirstOrDefault(contactorder => contactorder.OrderId == OrderId && contactorder.ContactId == ContactId);
            await _repositoryFactory.Instantiate<ContactOrderEntity>().RemoveEntityAsync(contactOrder);
            TempData["ConfirmModal"] = false;
            return RedirectToAction(ActionName, ControllerName, new { EntityId = OrderId });
        }
    }
}
