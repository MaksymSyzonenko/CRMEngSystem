namespace CRMEngSystem.Dto.Contact
{
    public sealed class ContactDto
    {
        public int ContactId { get; init; }
        public int EnterpriseId { get; init; }
        public byte[]? Image { get; init; }
        public string Initials { get; init; } = default!;
        public string EnterpriseNameUA { get; init; } = default!;
        public string EnterpriseAddress { get; init; } = default!;
        public string Position { get; init; } = default!;
        public DateTime DateTimeCreate { get; init; }
        public DateTime DateTimeUpdate { get; init; }
        public string FirstPhoneNumber { get; init; } = default!;
        public string ExtraPhoneNumber { get; init; } = default!;
        public string FirstEmail { get; init; } = default!;
        public string ExtraEmail { get; init; } = default!;
        public int NumberHighPriorityOrders { get; init; }
        public int NumberMediumPriorityOrders { get; init; }
        public int NumberLowPriorityOrders { get; init; }
        public int TotalOrdersNumber { get; init; }
        public string? TelegramLink { get; init; }
        public string? LinkedInLink { get; init; }
        public string? ViberLink { get; init; }
        public string? FaceBookLink { get; init; }
        public string? WhatsappLink { get; init; }
        public string? SkypeLink { get; init; }
        public string? TwitterLink { get; init; }
        public string? InstagramLink { get; init; }
    }
}
