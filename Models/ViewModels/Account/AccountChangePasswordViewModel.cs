namespace CRMEngSystem.Models.ViewModels.Account
{
    public sealed class AccountChangePasswordViewModel
    {
        public string OldPassword { get; init; } = default!;
        public string NewPassword { get; init; } = default!;
    }
}
