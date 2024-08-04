using CRMEngSystem.Data.Entities.Catalog;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.DefaultData.Catalog;

namespace CRMEngSystem.Data.Configurations.Catalog
{
    public sealed class CatalogPositionImageEntityConfiguration : IEntityTypeConfiguration<CatalogPositionImageEntity>
    {
        public void Configure(EntityTypeBuilder<CatalogPositionImageEntity> builder)
        {
            builder.HasKey(image => image.CatalogPositionImageId);

            //builder.HasData(EquipmentCatalogPositionDefaultData.CatalogPositionImageDefaultData);
        }
    }
}
