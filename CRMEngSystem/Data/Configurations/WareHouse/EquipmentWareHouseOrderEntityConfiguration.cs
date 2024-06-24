using CRMEngSystem.Data.Entities.WareHouse;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Data.Configurations.WareHouse
{
    public sealed class EquipmentWareHouseOrderEntityConfiguration : IEntityTypeConfiguration<EquipmentWareHouseOrderEntity>
    {
        public void Configure(EntityTypeBuilder<EquipmentWareHouseOrderEntity> builder)
        {
            builder.HasKey(equipment => equipment.EquipmentWareHouseOrderId);

            builder.HasOne(equipment => equipment.WareHouse)
                .WithMany(warehouse => warehouse.EquipmentWareHouseOrder)
                .HasForeignKey(equipment => equipment.WareHouseId);

            builder.HasOne(equipment => equipment.EquipmentOrderPosition)
                .WithMany(equipment => equipment.EquipmentWareHouseOrder)
                .HasForeignKey(equipment => equipment.EquipmentOrderPositionId);
        }
    }
}
