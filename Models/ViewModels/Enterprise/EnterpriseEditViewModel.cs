namespace CRMEngSystem.Models.ViewModels.Enterprise
{
    public sealed class EnterpriseEditViewModel
    {
        public int EnterpriseId { get; init; }
        public string NameUA { get; init; } = default!;
        public string NameEN { get; init; } = default!;
        public string OwnershipFormUA { get; init; } = default!;
        public string OwnershipFormEN { get; init; } = default!;
        public string EDRPOU { get; init; } = default!;
        public string Link { get; init; } = default!;
        public string IndustryBranch { get; init; } = default!;
        public string TradeGroup { get; init; } = default!;
        public string Franchise { get; init; } = default!;
        public string Country { get; init; } = default!;
        public string City { get; init; } = default!;
        public string Region { get; init; } = default!;
        public string PostalCode { get; init; } = default!;
        public string Street { get; init; } = default!;
        public string Coordinate { get; init; } = default!;
        public string CountryCode { get; init; } = default!;
    }
}
