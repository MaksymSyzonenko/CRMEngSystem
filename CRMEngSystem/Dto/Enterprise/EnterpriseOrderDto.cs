namespace CRMEngSystem.Dto.Enterprise
{
    public sealed class EnterpriseOrderDto
    {
        public int OrderId { get; init; }
        public string Priority { get; init; } = default!;
        public decimal TotalSellPrices { get; init; }
        public string InitiatorInitials { get; init; } = default!;
        public int InitiatorId { get; init; }
        public DateTime DateTimeCreate { get; init; }
        public string Status { get; init; } = default!;
    }
}
