namespace CRMEngSystem.Dto.Contact
{
    public sealed class ContactSelectDto
    {
        public int ContactId { get; init; }
        public byte[]? Image { get; init; }
        public string Initials { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string PhoneNumber { get; init; } = default!;
        public bool IsSelected { get; init; }
    }
}
