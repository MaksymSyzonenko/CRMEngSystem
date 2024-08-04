using CRMEngSystem.Data.Entities.Core;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.WareHouse;
using CRMEngSystem.Data.Enums;

namespace CRMEngSystem.Data.Entities.Catalog
{
    public class EquipmentCatalogPositionEntity : IEntity
    {
        public int EquipmentCatalogPositionId { get; set; } 

        //Data Properties
        public string EquipmentCode { get; set; } = default!;
        public string NameUA { get; set; } = default!;
        public string NameEN { get; set; } = default!;
        public EquipmentTypeValue Type { get; set; }
        public string? Producer { get; set; }
        public string? Country { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime DateTimeCreate { get; set; }
        public DateTime? DateTimeUpdate { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public string? Link { get; set; }

        //Navigation Properties
        public virtual CatalogPositionImageEntity? Image { get; set; }
        public virtual ICollection<EquipmentOrderPositionEntity>? EquipmentOrderPositions { get; set; }
        public virtual ICollection<EquipmentWareHousePositionEntity>? EquipmentWareHousePositions { get; set; }
    }
}
