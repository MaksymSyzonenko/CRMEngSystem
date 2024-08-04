using CRMEngSystem.Data.Enums;

namespace CRMEngSystem.Dto.Account
{
    public sealed class AccountListItemDto
    {
        public int ContactId { get; init; }
        public string Login { get; init; } = default!;
        public string Initials { get; init; } = default!;
        public AccessLevel AccessLevel { get; init; }
    }
}
