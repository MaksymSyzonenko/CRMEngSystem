using CRMEngSystem.Dto.Contact;

namespace CRMEngSystem.Models.ViewModels.Order
{
    public sealed class OrderContactsViewModel : OrderMenuTabViewModel
    {
        public IEnumerable<ContactListItemDto> Contacts { get; init; } = default!;
    }
}
