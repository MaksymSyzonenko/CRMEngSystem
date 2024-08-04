using CRMEngSystem.Data.Enums;

namespace CRMEngSystem.Dto.Account
{
    public sealed class AccountDto
    {
        public string UserName { get; init; } = default!;
        public AccessLevel AccessLevel { get; init; }
    }
}
