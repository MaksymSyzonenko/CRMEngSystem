using CRMEngSystem.Dto.Contact;
using CRMEngSystem.Dto.Order;

namespace CRMEngSystem.Models.ViewModels.Home
{
    public sealed class HomeViewModel
    {
        public IEnumerable<OrderListItemDto> Orders { get; set; } = default!;
        public IEnumerable<ContactListItemDto> Contacts { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Currency { get; set; } = default!;
        public decimal Value { get; set; }
        public decimal UAH_GBP_Currency { get; set; }
        public decimal UAH_EUR_Currency { get; set; }
        public decimal UAH_USD_Currency { get; set; }
        public decimal UAH_EUR_RateChanges { get; set; }
        public decimal UAH_USD_RateChanges { get; set; }
        public decimal UAH_GBP_RateChanges { get; set; }
        public decimal GBP_Value { get; set; }
        public decimal EUR_Value { get; set; }
        public decimal USD_Value { get; set; }
        public decimal UAH_Value { get; set; }
        public decimal BasePrice { get; set; }
        public int Discount { get; set; }
        public int MarkUp { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellPrice { get; set; }
        public string EquipmentCode { get; set; } = default!;
        public string EquipmentNameEN { get; set; } = default!;
    }
}
