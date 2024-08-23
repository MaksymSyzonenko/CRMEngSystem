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
        public string? Link { get; init; }
        public string EDRPOU { get; init; } = default!;
        public string CountryCode { get; init; } = default!;
    }
}
