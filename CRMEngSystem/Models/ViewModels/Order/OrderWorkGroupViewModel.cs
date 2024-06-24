using CRMEngSystem.Dto.Contact;

namespace CRMEngSystem.Models.ViewModels.Order
{
    public sealed class OrderWorkGroupViewModel : OrderMenuTabViewModel
    {
        public IEnumerable<ContactListItemDto> Contacts { get; init; } = default!;
    }
}
