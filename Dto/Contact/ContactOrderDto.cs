namespace CRMEngSystem.Dto.Contact
{
    public class ContactOrderDto
    {
        public int ContactId { get; init; }
        public int EnterpriseId { get; init; }
        public byte[]? Image { get; init; }
        public string Initials { get; init; } = default!;
        public string EnterpriseNameUA { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string PhoneNumber { get; init; } = default!;
        public bool IsOfferRecipient { get; set; }
    }
}
