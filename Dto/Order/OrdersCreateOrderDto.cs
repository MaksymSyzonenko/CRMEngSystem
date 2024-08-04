namespace CRMEngSystem.Dto.Order
{
    public sealed class OrdersCreateOrderDto
    {
        public int OrderId { get; init; }
        public string CustomerNameUA { get; init; } = default!;
        public int CustomerId { get; init; }
        public string Priority { get; init; } = default!;
        public bool IsSelected { get; init; }
        public bool IncludeWareHouse { get; init; }
    }
}
