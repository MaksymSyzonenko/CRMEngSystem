using CRMEngSystem.Dto.Contact;

namespace CRMEngSystem.Models.ViewModels.Enterprise
{
    public sealed class EnterpriseContactsViewModel : EnterpriseMenuTabViewModel
    {
        public IEnumerable<ContactListItemDto> Contacts { get; init; } = default!;
    }
}
