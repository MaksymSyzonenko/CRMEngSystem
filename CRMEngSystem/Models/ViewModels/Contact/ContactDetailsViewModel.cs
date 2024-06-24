using CRMEngSystem.Dto.Contact;

namespace CRMEngSystem.Models.ViewModels.Contact
{
    public sealed class ContactDetailsViewModel : ContactMenuTabViewModel
    {
        public ContactDto Contact { get; init; } = default!;
    }
}
