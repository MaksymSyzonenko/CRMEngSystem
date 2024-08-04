using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.DefaultData.Order;

namespace CRMEngSystem.Data.Configurations.Order
{
    public sealed class EquipmentOrderPositionEntityConfiguration : IEntityTypeConfiguration<EquipmentOrderPositionEntity>
    {
        public void Configure(EntityTypeBuilder<EquipmentOrderPositionEntity> builder)
        {
            builder.HasKey(equipment => equipment.EquipmentOrderPositionId);

            builder.HasOne(equipment => equipment.Order)
                .WithMany(order => order.EquipmentOrderPositions)
                .HasForeignKey(equipment => equipment.OrderId);

            //builder.HasData(EquipmentOrderPositionDefaultData.EquipmentPositions);
        }
    }
}
