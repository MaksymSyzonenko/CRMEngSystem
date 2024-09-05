using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Loaders.Contact;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Order;
using CRMEngSystem.Models.ViewModels.Order;
using CRMEngSystem.Services.Mail;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderAccountingController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        public OrderAccountingController(IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager)
        {
            _repositoryFactory = repositoryFactory;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> OrderAccounting(int EntityId)
        {
            var entity = await _repositoryFactory.Instantiate<OrderEntity>().GetEntityAsync(new OrderDataLoader(true, true, true, true, true), entity => entity.OrderId, EntityId);
            return View(new OrderAccountingViewModel
            {
                ShippingCost = entity.EquipmentOrderPositions.Sum(equipment => equipment.Quantity * equipment.ShippingCost),
                EntityId = entity!.OrderId,
                ActiveTab = "Accounting",
                NumberEquipmentPositions = entity.EquipmentOrderPositions != null ? entity.EquipmentOrderPositions.Count : 0,
                NumberContacts = entity.ContactOrders != null ? entity.ContactOrders.Where(contactorder => contactorder.Contact.EnterpriseId != 1).Count() : 0,
                NumberWorkGroup = entity.ContactOrders != null ? entity.ContactOrders.Where(contactorder => contactorder.Contact.EnterpriseId == 1).Count() : 0,
                NumberComments = entity.Comments != null ? entity.Comments.Count : 0
            });
        }
        [HttpPost]
        public async Task<IActionResult> OrderAccounting(OrderAccountingViewModel model)
        {
            string shippingCostString = Request.Form["ShippingCost"]!;
            decimal shippingCostResult = 0;
            if (!string.IsNullOrEmpty(shippingCostString))
            {
                shippingCostString = shippingCostString.Replace(',', '.');
                if (decimal.TryParse(shippingCostString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal outShippingCost))
                    shippingCostResult = outShippingCost;
            }

            var order = await _repositoryFactory
                .Instantiate<OrderEntity>()
                .GetEntityAsync(new OrderDataLoader(true, false, false, false, true), order => order.OrderId, model.EntityId);

            string subject = $"Комерційна Пропозиція №{model.EntityId} - рахунок для \"{order.Customer.Details.OwnershipFormUA}, {order.Customer.Details.NameUA}\"";

            bool isShippingCostChanged = shippingCostResult != order.EquipmentOrderPositions.Sum(equipment => equipment.ShippingCost * equipment.Quantity);
            decimal shippingCost = !model.IncludeShippingCost ? order.EquipmentOrderPositions.Sum(equipment => equipment.ShippingCost * equipment.Quantity) : 0;
            decimal totalSumPrice = 0;

            var equipmentListString = new StringBuilder();
            foreach (var equipment in order.EquipmentOrderPositions)
            {
                if (model.IncludeShippingCost)
                {
                    if (isShippingCostChanged)
                    {
                        decimal proportion = equipment.ShippingCost / order.EquipmentOrderPositions.Sum(equipment => equipment.ShippingCost);
                        equipmentListString.AppendLine(
                            $"<tr><td style='text-align: center; padding: 5px;'>{equipment.EquipmentCatalogPosition.EquipmentCode}</td>" +
                            $"<td style='padding: 5px 15px;'>{equipment.EquipmentCatalogPosition.NameUA}</td>" +
                            $"<td style='text-align: center; padding: 5px;'>{Math.Round(equipment.SellPrice + (proportion * shippingCostResult / equipment.Quantity), 2)}</td>" +
                            $"<td style='text-align: center; padding: 5px;'>{equipment.Quantity},00</td>" +
                            $"<td style='text-align: center; padding: 5px;'>{Math.Round((equipment.SellPrice + (proportion * shippingCostResult / equipment.Quantity)) * equipment.Quantity, 2)}</td></tr>"
                        );
                        totalSumPrice += (equipment.SellPrice + (proportion * shippingCostResult / equipment.Quantity)) * equipment.Quantity;
                    }
                    else
                    {
                        equipmentListString.AppendLine(
                            $"<tr><td style='text-align: center; padding: 5px;'>{equipment.EquipmentCatalogPosition.EquipmentCode}</td>" +
                            $"<td style='padding: 5px 15px;'>{equipment.EquipmentCatalogPosition.NameUA}</td>" +
                            $"<td style='text-align: center; padding: 5px;'>{equipment.SellPrice + equipment.ShippingCost}</td>" +
                            $"<td style='text-align: center; padding: 5px;'>{equipment.Quantity},00</td>" +
                            $"<td style='text-align: center; padding: 5px;'>{(equipment.SellPrice + equipment.ShippingCost) * equipment.Quantity}</td></tr>"
                        );
                        totalSumPrice += (equipment.SellPrice + equipment.ShippingCost) * equipment.Quantity;
                    }
                }
                else
                {
                    equipmentListString.AppendLine(
                        $"<tr><td style='text-align: center; padding: 5px;'>{equipment.EquipmentCatalogPosition.EquipmentCode}</td>" +
                        $"<td style='padding: 5px 15px;'>{equipment.EquipmentCatalogPosition.NameUA}</td>" +
                        $"<td style='text-align: center; padding: 5px;'>{equipment.SellPrice}</td>" +
                        $"<td style='text-align: center; padding: 5px;'>{equipment.Quantity},00</td>" +
                        $"<td style='text-align: center; padding: 5px;'>{equipment.SellPrice * equipment.Quantity}</td></tr>"
                    );
                    totalSumPrice += equipment.SellPrice * equipment.Quantity;
                }
            }

            var user = await _repositoryFactory.Instantiate<ContactEntity>().GetEntityAsync(
                new ContactDataLoader(true, false, false, false, false),
                contact => contact.ContactId,
                (await _userManager.GetUserAsync(User))!.ContactId
            );

            string senderName = $"{user.Details.LastName} {user.Details.FirstName} {user.Details.MiddleName}";

            var body = new StringBuilder();
            body.AppendLine("Доброго дня.<br>")
                .AppendLine($"Прошу зробити рахунок для \"{order.Customer.Details.OwnershipFormUA}, {order.Customer.Details.NameUA}\", згідно переліку нижче{(shippingCost == 0 ? " (Доставку включено до кожної позиції)" : string.Empty)}:")
                .AppendLine($"<br>Курс: {model.Currency}.");
              
            if (!string.IsNullOrEmpty(model.Note)) body.AppendLine($"<br>{model.Note}");

            body.AppendLine($"<table border='1' style='border-collapse: collapse; width: 80%;'>")
                .AppendLine("<thead><tr><th style='width: 12%; padding: 5px;'>Код</th><th style='width: 48%; text-align: left; padding: 5px 15px;'>Найменування</th><th style='width: 14%; padding: 5px;'>Ціна, €</th><th style='width: 10%; padding: 5px;'>К-сть, шт</th><th style='width: 16%; padding: 5px;'>Загальна ціна, €</th></tr></thead>")
                .AppendLine($"<tbody>{equipmentListString}")
                .AppendLine($"<tr><td colspan='2' style='text-align: right; padding: 5px; font-weight: bold;'>Разом:</td><td style='text-align: center; padding: 5px 15px; font-weight: bold;'>—</td><td style='text-align: center; padding: 5px; font-weight: bold;'>{order.EquipmentOrderPositions.Sum(equipment => equipment.Quantity)} шт</td><td style='text-align: center; padding: 5px; font-weight: bold;'>{Math.Round(totalSumPrice, 2)} €</td></tr>")
                .AppendLine("</tbody></table>");

            body.AppendLine($"<br><h4>З повагою, {senderName}<h4>");

            string finalBody = body.ToString();

            if(SendMailService.Send(subject, finalBody, "stimex.orders@gmail.com", model.Receiver, model.ExtraReceiver))
            {
                TempData["NotifyModal"] = true;
                TempData["NotifyText"] = "Лист успішно надіслано.";
            }
            else
            {
                TempData["ErrorNotifyModal"] = true;
                TempData["NotifyText"] = "Сталася помилка при відправці листа!";
            }
            
            return RedirectToAction("OrderAccounting", "OrderAccounting", new { model.EntityId });
        }
    }
}
