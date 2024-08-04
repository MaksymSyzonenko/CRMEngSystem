using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.DefaultData.Catalog;
using CRMEngSystem.Data.Entities.Catalog;

namespace CRMEngSystem.Data.Configurations.Catalog
{
    public sealed class EquipmentCatalogPositionEntityConfiguration : IEntityTypeConfiguration<EquipmentCatalogPositionEntity>
    {
        public void Configure(EntityTypeBuilder<EquipmentCatalogPositionEntity> builder)
        {
            builder.HasKey(equipment => equipment.EquipmentCatalogPositionId);

            builder.HasOne(equipment => equipment.Image)
                .WithOne(image => image.EquipmentCatalogPosition)
                .HasForeignKey<CatalogPositionImageEntity>(image => image.EquipmentCatalogPositionId)
                .IsRequired();

            builder.HasMany(equipment => equipment.EquipmentOrderPositions)
                .WithOne(equipment => equipment.EquipmentCatalogPosition)
                .HasForeignKey(equipment => equipment.EquipmentCatalogPositionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(equipment => equipment.EquipmentWareHousePositions)
                .WithOne(equipment => equipment.EquipmentCatalogPosition)
                .HasForeignKey(equipment => equipment.EquipmentCatalogPositionId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.HasData(EquipmentCatalogPositionDefaultData.EquipmentCatalogPositions);
        }
    }
}
