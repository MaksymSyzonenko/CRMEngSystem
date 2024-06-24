using CRMEngSystem.Data.Entities.Core;

namespace CRMEngSystem.Data.Entities.Catalog
{
    public class CatalogPositionImageEntity : IEntity
    {
        public int CatalogPositionImageId { get; set; }

        //Data Properties
        public byte[]? Bytes { get; set; }

        //Navigation Properties
        public int EquipmentCatalogPositionId { get; set; } = default!;
        public virtual EquipmentCatalogPositionEntity EquipmentCatalogPosition { get; set; } = null!;
    }
}
