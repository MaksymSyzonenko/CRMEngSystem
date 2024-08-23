using CRMEngSystem.Data.Entities.Core;

namespace CRMEngSystem.Data.Entities.Enterprise
{
    public class EnterpriseDetailsEntity : IEntity
    {
        //Data Properties
        public string NameUA { get; set; } = default!;
        public string NameEN { get; set; } = default!;
        public string OwnershipFormUA { get; set; } = default!;
        public string OwnershipFormEN { get; set; } = default!;
        public string EDRPOU { get; set; } = default!;
        public string? Link { get; set; } = default!;
        public string IndustryBranch { get; set; } = default!;
        public string TradeGroup { get; set; } = default!;
        public string? Franchise { get; set; }
        public string Country { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Region { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string? Coordinate { get; set; } = default!;
        public string CountryCode { get; set; } = default!;
        //Navigation Properties
        public int EnterpriseId { get; set; }
        public virtual EnterpriseEntity Enterprise { get; set; } = null!;
    }
}
