namespace CRMEngSystem.Dto.Enterprise
{
    public sealed record class EnterpriseListItemDto
    {
        public int EnterpriseId { get; init; }
        public string NameUA { get; init; } = default!;
        public string Country { get; init; } = default!;
        public string Region { get; init; } = default!;
        public string City { get; init; } = default!;
        public string Coordinate { get; init; } = default!;
    }
}
