using CRMEngSystem.Dto.Account;
using CRMEngSystem.Models.ViewModels.Contact;

namespace CRMEngSystem.Models.ViewModels.Account
{
    public sealed class AccountDetailsViewModel : ContactMenuTabViewModel
    {
        public AccountDto Account { get; init; } = default!;
    }
}
