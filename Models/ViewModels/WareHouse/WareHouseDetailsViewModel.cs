using CRMEngSystem.Data.Enums;
using CRMEngSystem.Dto.WareHouse;
using CRMEngSystem.Models.ViewModels.Pagination;

namespace CRMEngSystem.Models.ViewModels.WareHouse
{
    public sealed class WareHouseDetailsViewModel : PaginationViewModel
    {
        public int WareHouseId { get; set; }
        public IEnumerable<EquipmentWareHousePositionDto> EquipmentWareHousePositions { get; set; } = default!;
        public string WareHouseName { get; set; } = default!;
        public string? Coordinate { get; set; }
        public string Country { get; set; } = default!;
        public string Region { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public string SearchGeneral { get; set; } = default!;
        public int TotalQuantity { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }
}
