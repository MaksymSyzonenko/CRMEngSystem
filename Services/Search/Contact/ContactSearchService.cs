using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Services.Formatter;
using CRMEngSystem.Services.Search.Core;

namespace CRMEngSystem.Services.Search.Contact
{
    public sealed class ContactSearchService : ISearchService<ContactEntity>
    {
        private readonly string? _searchGeneral;
        public ContactSearchService(string? searchGeneral)
        {
            _searchGeneral = searchGeneral;
        }

        public IQueryable<ContactEntity> Search(IQueryable<ContactEntity> entities)
        {
            var contacts = new List<ContactEntity>();
            if (!string.IsNullOrEmpty(_searchGeneral))
            {
                foreach (var entity in entities)
                {
                    if ((entity.Enterprise.Details.NameUA != null && entity.Enterprise.Details.NameUA.ToLower().Contains(_searchGeneral.ToLower())) ||
                    (entity.Enterprise.Details.NameEN != null && entity.Enterprise.Details.NameEN.ToLower().Contains(_searchGeneral.ToLower())) ||
                    (entity.Details.FirstName != null && entity.Details.FirstName.ToLower().Contains(_searchGeneral.ToLower())) ||
                    (entity.Details.LastName != null && entity.Details.LastName.ToLower().Contains(_searchGeneral.ToLower())) ||
                    (entity.Details.MiddleName != null && entity.Details.MiddleName.ToLower().Contains(_searchGeneral.ToLower())) ||
                    (entity.Details.FirstPhoneNumber != null && PhoneNumberFormatter.IsValidPhoneNumber(_searchGeneral, entity.Details.FirstPhoneNumber)) ||
                    (entity.Details.FirstEmail != null && entity.Details.FirstEmail.ToLower().Contains(_searchGeneral.ToLower())) ||
                    (entity.Details.Position != null && entity.Details.Position.ToLower().Contains(_searchGeneral.ToLower())) ||
                    (entity.Details.ExtraPhoneNumber != null && PhoneNumberFormatter.IsValidPhoneNumber(_searchGeneral, entity.Details.ExtraPhoneNumber)) ||
                    (entity.Details.ExtraEmail != null && entity.Details.ExtraEmail.ToLower().Contains(_searchGeneral.ToLower())))
                    {
                        contacts.Add(entity);
                    }
                }
            }
            else
            {
                return entities;
            }

            return contacts.AsQueryable();
        }
    }
}
