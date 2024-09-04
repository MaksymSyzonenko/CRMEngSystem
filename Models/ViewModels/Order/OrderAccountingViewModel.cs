namespace CRMEngSystem.Models.ViewModels.Order
{
    public sealed class OrderAccountingViewModel : OrderMenuTabViewModel
    {
        public string Receiver { get; init; } = "elena.gavenis@gmail.com";
        public string ExtraReceiver { get; init; } = default!;
        public bool IncludeShippingCost { get; init; }
        public string Currency { get; init; } = default!;
        public string Note { get; init; } = default!; 
        public decimal ShippingCost { get; init; }
    }
}
