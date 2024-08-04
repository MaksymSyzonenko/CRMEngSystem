using CRMEngSystem.Data.Enums;
using CRMEngSystem.Dto.Account;

namespace CRMEngSystem.Models.ViewModels.Control
{
    public sealed class ControlDetailsViewModel
    {
        public IEnumerable<AccountListItemDto> Accounts { get; init; } = default!;
        public int CurrentUserId { get; init; }
        public decimal CurrencyCoefficient { get; init; }
        public decimal CurrencyCoefficient_UAH_EUR { get; init; }
        public decimal ShippingRatePerKg { get; init; }
        public decimal ShippingRatePerCubicMeter { get; init; }
        public AccessLevel AccessLevel { get; init; }
    }
}
