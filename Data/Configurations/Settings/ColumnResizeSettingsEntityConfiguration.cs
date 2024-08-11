using CRMEngSystem.Data.Entities.Settings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Data.Configurations.Settings
{
    public class ColumnResizeSettingsEntityConfiguration : IEntityTypeConfiguration<ColumnResizeSettingsEntity>
    {
        public void Configure(EntityTypeBuilder<ColumnResizeSettingsEntity> builder)
        {
            builder.HasKey(setting => setting.ColumnResizeSettingsId);
        }
    }
}
