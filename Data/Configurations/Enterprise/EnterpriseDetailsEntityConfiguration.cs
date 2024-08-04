using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.DefaultData.Enterprise;
using CRMEngSystem.Data.Entities.Enterprise;

namespace CRMEngSystem.Data.Configurations.Enterprise
{
    public sealed class EnterpriseDetailsEntityConfiguration : IEntityTypeConfiguration<EnterpriseDetailsEntity>
    {
        public void Configure(EntityTypeBuilder<EnterpriseDetailsEntity> builder)
        {
            builder.HasKey(details => details.EnterpriseId);

            //builder.HasData(EnterpriseDetailsDefaultData.EnterprisesDetails);
        }
    }
}
