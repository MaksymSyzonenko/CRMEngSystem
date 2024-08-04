using CRMEngSystem.Data.Enums;

namespace CRMEngSystem.Dto.Order
{
    public sealed record class OrderDto
    {
        public int OrderId { get; init; }
        public OrderStatusValue Status { get; init; }
        public DateTime DateTimeProcessingStatusStart { get; init; }
        public DateTime? DateTimeOfferStatusStart { get; init; }
        public DateTime? DateTimeExecutionStatusStart { get; init; }
        public DateTime? DateTimeCompletedStatusStart { get; init; }
        public int CustomerId { get; init; }
        public string CustomerNameUA { get; init; } = default!;
        public int InitiatorId { get; init; }
        public string InitiatorInitials { get; init; } = default!;
        public string Priority { get; init; } = default!;
        public DateTime DateTimeCreate { get; init; }
        public DateTime DateTimeUpdate { get; init; }
        public string CustomerAddress { get; init; } = default!;
        public int NumberEquipmentPositions { get; init; }
        public double TotalWeight { get; init; }
        public double TotalVolume { get; init; }
        public decimal TotalBasePrices { get; init; }
        public decimal TotalPurchasePrices { get; init; }
        public decimal TotalSellPrices { get; init; }
        public decimal TotalShippingCosts { get; init; }
    }
}
