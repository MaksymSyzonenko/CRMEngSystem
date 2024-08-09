using CRMEngSystem.Dto.Contact;
using CRMEngSystem.Dto.Enterprise;

namespace CRMEngSystem.Models.ViewModels.Enterprise
{
    public sealed class EnterpriseDetailsViewModel : EnterpriseMenuTabViewModel
    {
        public EnterpriseDto Enterprise { get; init; } = default!;
        public IEnumerable<EnterpriseOrderDto> LastOrders { get; init; } = default!;
        public IEnumerable<ContactListItemDto> Contacts { get; init; } = default!;
    }
}
