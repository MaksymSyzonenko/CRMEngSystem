namespace CRMEngSystem.Models.ViewModels.WareHouse
{
    public sealed class WareHouseUpdatePositionsViewModel
    {
        public int WareHouseId { get; init; }
        public IFormFile File { get; init; } = default!;
    }
}
