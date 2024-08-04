using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.DefaultData.Contact;
using CRMEngSystem.Data.Entities.Contact;

namespace CRMEngSystem.Data.Configurations.Contact
{
    public sealed class ContactDetailsEntityConfiguration : IEntityTypeConfiguration<ContactDetailsEntity>
    {
        public void Configure(EntityTypeBuilder<ContactDetailsEntity> builder)
        {
            builder.HasKey(details => details.ContactId);

            //builder.HasData(ContactDetailsDefaultData.ContactsDetails);
        }
    }
}
