using CRMEngSystem.Dto.Enterprise;

namespace CRMEngSystem.Models.ViewModels.Enterprise
{
    public sealed class EnterpriseDetailsViewModel : EnterpriseMenuTabViewModel
    {
        public EnterpriseDto Enterprise { get; init; } = default!;
        public IEnumerable<int> NumberOrdersPerMonth { get; init; } = default!;
        public IEnumerable<decimal> TotalOrderAmounts { get; init; } = default!;
        public IEnumerable<string> Months { get; init; } = default!;
    }
}
