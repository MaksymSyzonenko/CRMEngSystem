using CRMEngSystem.Dto.Enterprise;

namespace CRMEngSystem.Models.ViewModels.Enterprise
{
    public sealed class EnterpriseOrdersViewModel : EnterpriseMenuTabViewModel
    {
        public IEnumerable<EnterpriseOrderDto> Orders { get; init; } = default!;
        public string? SearchOrderId { get; init; } = default!;
        public string? SearchInitiatorInitials { get; init; }
        public string? FilterPriority { get; init; } = default!;
        public string? FilterStatus { get; init; } = default!;
        public decimal? FilterMinSellPrice { get; init; }
        public decimal? FilterMaxSellPrice { get; init; }
        public DateOnly? FilterDateStart { get; init; }
        public DateOnly? FilterDateEnd { get; init; }
    }
}
