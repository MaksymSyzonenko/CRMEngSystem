namespace CRMEngSystem.Dto.Enterprise
{
    public sealed class EnterpriseDto
    {
        public string NameUA { get; init; } = default!;
        public string OwnershipFormUA { get; init; } = default!;
        public string NameEN { get; init; } = default!;
        public string OwnershipFormEN { get; init; } = default!;
        public string Address { get; init; } = default!;
        public string Coordinate { get; init; } = default!;
        public string IndustryBranch { get; init; } = default!;
        public string TradeGroup { get; init; } = default!;
        public string Franchise { get; init; } = default!;
        public DateTime DateTimeCreate { get; init; }
        public DateTime DateTimeUpdate { get; init; }
        public int NumberHighPriorityOrders { get; init; }
        public int NumberMediumPriorityOrders { get; init; }
        public int NumberLowPriorityOrders { get; init; }
        public int TotalOrdersNumber { get; init; }
    }
}
