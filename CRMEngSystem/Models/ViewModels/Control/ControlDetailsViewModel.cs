using CRMEngSystem.Data.Enums;

namespace CRMEngSystem.Models.ViewModels.Control
{
    public sealed class ControlDetailsViewModel
    {
        public decimal CurrencyCoefficient { get; init; }
        public decimal ShippingRatePerKg { get; init; }
        public decimal ShippingRatePerCubicMeter { get; init; }
        public AccessLevel AccessLevel { get; init; }
    }
}
