using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.Entities.WareHouse;

namespace CRMEngSystem.Data.Configurations.WareHouse
{
    public sealed class EquipmentWareHousePositionEntityConfiguration : IEntityTypeConfiguration<EquipmentWareHousePositionEntity>
    {
        public void Configure(EntityTypeBuilder<EquipmentWareHousePositionEntity> builder)
        {
            builder.HasKey(equipment => equipment.EquipmentWareHousePositionId);

            builder.HasOne(equipment => equipment.WareHouse)
                .WithMany(warehouse => warehouse.EquipmentWareHousePositions)
                .HasForeignKey(equipment => equipment.WareHouseId);
        }
    }
}
