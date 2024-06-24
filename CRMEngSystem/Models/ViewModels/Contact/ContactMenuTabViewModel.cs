namespace CRMEngSystem.Models.ViewModels.Contact
{
    public class ContactMenuTabViewModel
    {
        public int EntityId { get; init; }
        public string ActiveTab { get; init; } = default!;
        public int? NumberOrders { get; init; }
        public int? NumberComments { get; init; }
    }
}
