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
            entities = string.IsNullOrEmpty(_searchGeneral) ? entities : entities.Where(entity =>
                (entity.Details.FirstName != null && entity.Details.FirstName.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.LastName != null && entity.Details.LastName.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.MiddleName != null && entity.Details.MiddleName.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.FirstPhoneNumber != null && entity.Details.FirstPhoneNumber.Contains(PhoneNumberFormatter.FormatPhoneNumber(_searchGeneral))) ||
                (entity.Details.FirstEmail != null && entity.Details.FirstEmail.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.Position != null && entity.Details.Position.ToLower().Contains(_searchGeneral.ToLower())) ||
                (entity.Details.ExtraPhoneNumber != null && entity.Details.ExtraPhoneNumber.Contains(PhoneNumberFormatter.FormatPhoneNumber(_searchGeneral))) ||
                (entity.Details.ExtraEmail != null && entity.Details.ExtraEmail.ToLower().Contains(_searchGeneral.ToLower())));

            return entities;
        }
    }
}
