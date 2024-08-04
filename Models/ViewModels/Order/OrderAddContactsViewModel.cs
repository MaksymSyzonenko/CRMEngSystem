using CRMEngSystem.Dto.Contact; 

namespace CRMEngSystem.Models.ViewModels.Order
{
    public sealed class OrderAddContactsViewModel
    {
        public int OrderId { get; init; }
        public bool WorkGroup { get; init; }
        public List<ContactSelectDto> Contacts { get; init; } = default!;
    }
}
