using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.DefaultData.User;

namespace CRMEngSystem.Data.Configurations.User
{
    public sealed class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Ignore(user => user.Email);
            builder.Ignore(user => user.NormalizedEmail);
            builder.Ignore(user => user.EmailConfirmed);
            builder.Ignore(user => user.PhoneNumber);
            builder.Ignore(user => user.PhoneNumberConfirmed);
            builder.Ignore(user => user.LockoutEnd);

            //builder.HasData(UserDefaultData.Users);
        }
    }
}
