using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRMEngSystem.Data.DefaultData.Contact;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.User;

namespace CRMEngSystem.Data.Configurations.Contact
{
    public sealed class ContactEntityConfiguration : IEntityTypeConfiguration<ContactEntity>
    {
        public void Configure(EntityTypeBuilder<ContactEntity> builder)
        {
            builder.HasKey(contact => contact.ContactId);

            builder.HasOne(contact => contact.User)
                .WithOne(user => user.Contact)
                .HasForeignKey<UserEntity>(user => user.ContactId)
                .IsRequired();

            builder.HasOne(contact => contact.Image)
                .WithOne(image => image.Contact)
                .HasForeignKey<ContactImageEntity>(image => image.ContactId)
                .IsRequired();

            builder.HasOne(contact => contact.Details)
                .WithOne(details => details.Contact)
                .HasForeignKey<ContactDetailsEntity>(details => details.ContactId)
                .IsRequired();

            builder.HasOne(contact => contact.Enterprise)
                .WithMany(enterprise => enterprise.Contacts)
                .HasForeignKey(contact => contact.EnterpriseId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.HasData(ContactDefaultData.Contacts);
        }
    }
}
