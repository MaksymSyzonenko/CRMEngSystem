
namespace CRMEngSystem.Models.ViewModels.Contact
{
    public sealed class ContactCreateViewModel
    {
        public int EnterpriseId { get; init; }
        public string EnterpriseNameUA { get; init; } = default!;
        public IFormFile Image { get; init; } = default!;
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string MiddleName { get; init; } = default!;
        public string Position { get; init; } = default!;
        public string FirstPhoneNumber { get; init; } = default!;
        public string ExtraPhoneNumber { get; init; } = default!;
        public string FirstEmail { get; init; } = default!;
        public string ExtraEmail { get; init; } = default!;
        public string TelegramLink { get; init; } = default!;
        public string LinkedInLink { get; init; } = default!;
        public string ViberLink { get; init; } = default!;
        public string FaceBookLink { get; init; } = default!;
        public string WhatsappLink { get; init; } = default!;
        public string SkypeLink { get; init; } = default!;
        public string TwitterLink { get; init; } = default!;
        public string InstagramLink { get; init; } = default!;
    }
}
