using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.DefaultData.WareHouse;
using CRMEngSystem.Data.Entities.WareHouse;

namespace CRMEngSystem.Data.Configurations.WareHouse
{
    public sealed class WareHouseEntityConfiguration : IEntityTypeConfiguration<WareHouseEntity>
    {
        public void Configure(EntityTypeBuilder<WareHouseEntity> builder)
        {
            builder.HasKey(warehouse => warehouse.WareHouseId);

            //builder.HasData(WareHouseDefaultData.WareHouses);
        }
    }
}
