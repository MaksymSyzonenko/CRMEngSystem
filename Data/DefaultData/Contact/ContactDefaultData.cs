using CRMEngSystem.Data.Entities.Contact;

namespace CRMEngSystem.Data.DefaultData.Contact
{
    public static class ContactDefaultData
    {
        public static readonly List<ContactEntity> Contacts = new()
        {
            new ContactEntity()
            {
                ContactId = 1,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                EnterpriseId = 1,
            },
            new ContactEntity()
            {
                ContactId = 2,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                EnterpriseId = 1,
            },
            new ContactEntity()
            {
                ContactId = 3,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                EnterpriseId = 1,
            },
            new ContactEntity()
            {
                ContactId = 4,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                EnterpriseId = 2,
            },
            new ContactEntity()
            {
                ContactId = 5,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                EnterpriseId = 2,
            },
            new ContactEntity()
            {
                ContactId = 6,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                EnterpriseId = 2,
            },
            new ContactEntity()
            {
                ContactId = 7,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                EnterpriseId = 3,
            },
            new ContactEntity()
            {
                ContactId = 8,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                EnterpriseId = 3,
            },
            new ContactEntity()
            {
                ContactId = 9,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                EnterpriseId = 3,
            },
            new ContactEntity()
            {
                ContactId = 10,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                EnterpriseId = 4,
            },
            new ContactEntity()
            {
                ContactId = 11,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                EnterpriseId = 4,
            },
            new ContactEntity()
            {
                ContactId = 12,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                EnterpriseId = 4,
            },
            new ContactEntity()
            {
                ContactId = 13,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                EnterpriseId = 5,
            },
            new ContactEntity()
            {
                ContactId = 14,
                DateTimeCreate = DateTime.Now,
                DateTimeUpdate = DateTime.Now,
                EnterpriseId = 5,
            }
        };
    }
}
