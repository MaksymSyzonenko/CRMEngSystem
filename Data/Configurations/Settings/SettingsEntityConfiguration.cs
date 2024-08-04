using CRMEngSystem.Data.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMEngSystem.Data.Configurations.Settings
{
    public class SettingsEntityConfiguration : IEntityTypeConfiguration<SettingsEntity>
    {
        public void Configure(EntityTypeBuilder<SettingsEntity> builder) 
        {
            builder.HasKey(setting => setting.SettingId);
        }
    }
}
