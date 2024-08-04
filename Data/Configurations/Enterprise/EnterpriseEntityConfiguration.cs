using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.DefaultData.Enterprise;
using CRMEngSystem.Data.Entities.Enterprise;

namespace CRMEngSystem.Data.Configurations.Enterprise
{
    public sealed class EnterpriseEntityConfiguration : IEntityTypeConfiguration<EnterpriseEntity>
    {
        public void Configure(EntityTypeBuilder<EnterpriseEntity> builder)
        {
            builder.HasKey(enterprise => enterprise.EnterpriseId);

            builder.HasMany(enterprise => enterprise.Orders)
                .WithOne(order => order.Customer)
                .HasForeignKey(order => order.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
           

            builder.HasOne(enterprise => enterprise.Details)
                .WithOne(details => details.Enterprise)
                .HasForeignKey<EnterpriseDetailsEntity>(details => details.EnterpriseId)
                .IsRequired();

            //builder.HasData(EnterpriseDefaultData.Enterprises);
        }
    }
}
