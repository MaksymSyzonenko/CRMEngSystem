using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.User;
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
        public async Task<IActionResult> OrderCreateOffer(int OrderId)
        {
            var order = await _repositoryFactory.Instantiate<OrderEntity>().GetEntityAsync(new OrderDataLoader(true, false, true, false, true), order => order.OrderId, OrderId);
            if(order.ContactOrders.All(contactorder => contactorder.Contact.EnterpriseId == 1))
            {
                TempData["ErrorNotifyModal"] = true;
                TempData["NotifyModal"] = false;
                TempData["ConfirmModal"] = false;
                TempData["NotifyText"] = "Щоб створити комерційну пропозицію потрібно додати хоча б 1 контакт до замовлення!";
                return RedirectToAction("OrderDetails", "OrderDetails", new { EntityId = OrderId });
            }
            
            string templatePath = "OfferPattern.docx";
            string outputPath = Guid.NewGuid() + ".docx";
            System.IO.File.Copy(templatePath, outputPath, true);
            var equipmentList = new List<EquipmentEntry>();
            foreach(var equipment in order.EquipmentOrderPositions)
            {
                equipmentList.Add(new EquipmentEntry
                {
                    Name = equipment.EquipmentCatalogPosition.NameUA,
                    Quantity = equipment.Quantity,
                    Price = equipment.SellPrice
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
            var contact = order.ContactOrders.Select(contacts => contacts.Contact).Where(contact => contact.EnterpriseId != 1).FirstOrDefault();
            var recipient = new Recipient()
            {
                RecipientFirstName = contact.Details.FirstName,
                RecipientLastName = contact.Details.LastName,
                RecipientEmail = contact.Details.FirstEmail,
                RecipientPhoneNumber = contact.Details.FirstPhoneNumber,
                RecipientOrganizationName = order.Customer.Details.NameEN
            };
            PriceValues priceValues = new() { TotalSum = equipmentList.Sum(e => e.Quantity * e.Price), ShippingCost = order.EquipmentOrderPositions.Sum(equipment => equipment.ShippingCost), TotalWithShippingCost = equipmentList.Sum(e => e.Quantity * e.Price) + order.EquipmentOrderPositions.Sum(equipment => equipment.ShippingCost) };
            CommercialOfferCreater offer = new(outputPath, recipient, sender, DateTime.Now, "EUR", priceValues, equipmentList);
            offer.CreateCommercialOffer();
            byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);
            string fileName = $"Комерційна_Пропозиція_{OrderId}.docx";
            System.IO.File.Delete(outputPath);
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }
    }
}
