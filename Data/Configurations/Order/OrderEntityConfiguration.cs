using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.DefaultData.Order;
using CRMEngSystem.Data.Entities.Order;

namespace CRMEngSystem.Data.Configurations.Order
{
    public sealed class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(order => order.OrderId);

            builder.HasOne(order => order.Initiator)
                .WithMany()
                .HasForeignKey(order => order.InitiatorId);

            //builder.HasData(OrderDefaultData.Orders);
        }
    }
}
