using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Loaders.Core;
using Microsoft.EntityFrameworkCore;

namespace CRMEngSystem.Data.Loaders.Order
{
    public sealed class ContactOrderDataLoader : IEntityDataLoader<ContactOrderEntity>
    {
        public bool Order { get; set; }
        public bool Contact { get; set; }
        public ContactOrderDataLoader(bool order, bool contact)
        {
            Order = order;
            Contact = contact;
        }

        public IQueryable<ContactOrderEntity> LoadData(IQueryable<ContactOrderEntity> query)
        {
            query = Order ? query.Include(contactorder => contactorder.Order) : query;
            query = Contact ? query.Include(contactorder => contactorder.Contact) : query;
            return query;
        }
    }
}
