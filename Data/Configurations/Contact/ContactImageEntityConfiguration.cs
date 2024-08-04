using CRMEngSystem.Data.DefaultData.Contact;
using CRMEngSystem.Data.Entities.Contact;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Data.Configurations.Contact
{
    public sealed class ContactImageEntityConfiguration : IEntityTypeConfiguration<ContactImageEntity>
    {
        public void Configure(EntityTypeBuilder<ContactImageEntity> builder)
        {
            builder.HasKey(image => image.ContactImageId);

            //builder.HasData(ContactImageDefaultData.ContactsImage);
        }
    }
}
