using CRMEngSystem.Dto.Contact;
using CRMEngSystem.Models.ViewModels.Pagination;

namespace CRMEngSystem.Models.ViewModels.Contact
{
    public sealed class ContactListViewModel : PaginationViewModel
    {
        public IEnumerable<ContactListItemDto> Entities { get; set; } = default!;
        public string? SearchGeneral { get; init; }
    }
}