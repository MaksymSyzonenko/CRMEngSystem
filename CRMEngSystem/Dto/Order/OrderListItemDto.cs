
namespace CRMEngSystem.Dto.Order
{
    public sealed record class OrderListItemDto
    {
        public int OrderId { get; init; }
        public int CustomerId { get; init; }
        public string CustomerNameUA { get; init; } = default!;
        public int InitiatorId { get; init; }
        public string InitiatorInitials { get; init;} = default!;
        public string Status { get; init; } = default!;
        public DateTime DateTimeCreate { get; init; }
        public string Priority { get; init; } = default!;
    }
}
