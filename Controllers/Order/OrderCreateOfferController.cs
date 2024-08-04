using CRMEngSystem.Configuration;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Enums;
using CRMEngSystem.Data.Loaders.Contact;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Word;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderCreateOfferController : Controller
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IRepositoryFactory _repositoryFactory;
        public OrderCreateOfferController(UserManager<UserEntity>  userManager, IRepositoryFactory repositoryFactory)
        {
            _userManager = userManager;
            _repositoryFactory = repositoryFactory;
        }
        public IActionResult OpenModal(int OrderId)
        {
            TempData["OrderChooseCurrencyConfirmModal"] = true;
            return RedirectToAction("OrderDetails", "OrderDetails", new { EntityId = OrderId });
        }
        public async Task<IActionResult> OrderCreateOffer(int OrderId, string Currency)
        {
            var order = await _repositoryFactory.Instantiate<OrderEntity>().GetEntityAsync(new OrderDataLoader(true, false, true, false, true), order => order.OrderId, OrderId);
            if (order.ContactOrders.All(contactorder => contactorder.Contact.EnterpriseId == 1))
            {
                TempData["ErrorNotifyModal"] = true;
                TempData["NotifyModal"] = false;
                TempData["ConfirmModal"] = false;
                TempData["NotifyText"] = "Щоб створити комерційну пропозицію потрібно додати хоча б 1 контакт до замовлення!";
                return RedirectToAction("OrderDetails", "OrderDetails", new { EntityId = OrderId });
            }
            if(order.ContactOrders.All(contactorder => !contactorder.IsOfferRecipient))
            {
                TempData["ErrorNotifyModal"] = true;
                TempData["NotifyModal"] = false;
                TempData["ConfirmModal"] = false;
                TempData["NotifyText"] = "Щоб створити комерційну пропозицію потрібно обрати отримувача!";
                return RedirectToAction("OrderDetails", "OrderDetails", new { EntityId = OrderId });
            }
            decimal coefficient = Currency == "UAH" ? ConfigurationSettings.CurrencyCoefficient_UAH_EUR : 1;
            var equipmentList = new List<EquipmentEntry>();
            foreach (var equipment in order.EquipmentOrderPositions)
            {
                equipmentList.Add(new EquipmentEntry
                {
                    Name = equipment.EquipmentCatalogPosition.NameUA,
                    Quantity = equipment.Quantity,
                    Price = Math.Round(equipment.SellPrice * coefficient, 2)
                });
            }
            var user = await _repositoryFactory.Instantiate<ContactEntity>().GetEntityAsync(new ContactDataLoader(true, false, false, false, false), contact => contact.ContactId, (await _userManager.GetUserAsync(User))!.ContactId);
            var sender = new Sender()
            {
                SenderFirstName = user.Details.FirstName,
                SenderLastName = user.Details.LastName,
                SenderEmail = user.Details.FirstEmail,
                SenderPhoneNumber = user.Details.FirstPhoneNumber,
                SenderProducerName = "Spirax Sarco"
            };

            var contact = order.ContactOrders
                .Where(contactorder => contactorder.IsOfferRecipient)
                .Select(contacts => contacts.Contact)
                .Where(contact => contact.EnterpriseId != 1)
                .FirstOrDefault();

            var recipient = new Recipient()
            {
                RecipientFirstName = contact.Details.FirstName,
                RecipientLastName = contact.Details.LastName,
                RecipientEmail = contact.Details.FirstEmail,
                RecipientPhoneNumber = contact.Details.FirstPhoneNumber,
                RecipientOrganizationName = order.Customer.Details.NameUA
            };
            PriceValues priceValues = new() { TotalSum = Math.Round(equipmentList.Sum(e => e.Quantity * e.Price), 2), ShippingCost = Math.Round(order.EquipmentOrderPositions.Sum(equipment => equipment.ShippingCost * equipment.Quantity) * coefficient, 2), TotalWithShippingCost = Math.Round(equipmentList.Sum(e => e.Quantity * e.Price) + order.EquipmentOrderPositions.Sum(equipment => equipment.ShippingCost * equipment.Quantity), 2) };
            byte[] fileBytes = Convert.FromBase64String(ConfigurationSettings.WordTemplate);
            CommercialOfferCreater offer = new(fileBytes, recipient, sender, DateTime.Now, Currency, priceValues, equipmentList);
            fileBytes = offer.CreateCommercialOffer();
            string fileName = $"Комерційна_Пропозиція_{OrderId}.docx";
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }
    }
}
