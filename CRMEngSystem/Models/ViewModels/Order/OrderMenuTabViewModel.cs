namespace CRMEngSystem.Models.ViewModels.Order
{
    public class OrderMenuTabViewModel
    {
        public int EntityId { get; init; }
        public string ActiveTab { get; init; } = default!;
        public int? NumberEquipmentPositions { get; init; }
        public int? NumberContacts { get; init; }
        public int? NumberWorkGroup { get; init; }
        public int? NumberComments { get; init; }
    }
}
