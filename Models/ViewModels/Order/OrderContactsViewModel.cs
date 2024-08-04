using CRMEngSystem.Dto.Contact;

namespace CRMEngSystem.Models.ViewModels.Order
{
    public sealed class OrderContactsViewModel : OrderMenuTabViewModel
    {
        public IEnumerable<ContactOrderDto> Contacts { get; init; } = default!;
    }
}
