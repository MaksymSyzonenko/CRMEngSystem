namespace CRMEngSystem.Models.ViewModels.Account
{
    public class AccountCreateViewModel
    {
        public int ContactId { get; init; }
        public string UserName { get; init; } = default!;
        public string Password { get; init; } = default!;
        public string AccessLevel { get; init; } = default!;
        public string ContactInitials { get; init; } = default!;
    }
}
