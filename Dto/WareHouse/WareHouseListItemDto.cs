namespace CRMEngSystem.Dto.WareHouse
{
    public sealed class WareHouseListItemDto
    {
        public int WareHouseId { get; init; }
        public string Name { get; init; } = default!;
        public string Country { get; init; } = default!;
        public string Region { get; init; } = default!;
        public string City { get; init; } = default!;
        public string Coordinate { get; init; } = default!;
        public string Street { get; init; } = default!;
        public string PostalCode { get; init; } = default!;
    }
}
