namespace CRMEngSystem.Models.ViewModels.WareHouse
{
    public sealed class WareHouseCreateViewModel
    {
        public string Name { get; init; } = default!;
        public string Country { get; init; } = default!;
        public string City { get; init; } = default!;
        public string Region { get; init; } = default!;
        public string PostalCode { get; init; } = default!;
        public string Street { get; init; } = default!;
        public string Coordinate { get; init; } = default!;
    }
}
