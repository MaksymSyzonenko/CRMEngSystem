using CRMEngSystem.Data.Entities.Enterprise;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Data.Configurations.Enterprise
{
    public class EnterpriseSelectEntityConfiguration : IEntityTypeConfiguration<EnterpriseSelectEntity>
    {
        public void Configure(EntityTypeBuilder<EnterpriseSelectEntity> builder)
        {
            builder.HasKey(select => select.EnterpriseSelectId);

        }
    }
}
