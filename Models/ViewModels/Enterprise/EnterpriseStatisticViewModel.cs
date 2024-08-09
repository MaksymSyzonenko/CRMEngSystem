namespace CRMEngSystem.Models.ViewModels.Enterprise
{
    public sealed class EnterpriseStatisticViewModel : EnterpriseMenuTabViewModel
    {
        public IEnumerable<int> NumberOrdersPerMonth { get; init; } = default!;
        public IEnumerable<decimal> TotalOrderAmounts { get; init; } = default!;
        public IEnumerable<string> Months { get; init; } = default!;
        public int NumberHighPriorityOrders { get; init; }
        public int NumberMediumPriorityOrders { get; init; }
        public int NumberLowPriorityOrders { get; init; }
        public int TotalOrdersNumber { get; init; }
    }
}
