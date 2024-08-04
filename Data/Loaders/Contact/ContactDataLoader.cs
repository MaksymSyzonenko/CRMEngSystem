using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Loaders.Core;

namespace CRMEngSystem.Data.Loaders.Contact
{
    public sealed class ContactDataLoader : IEntityDataLoader<ContactEntity>
    {
        public bool Details { get; init; }
        public bool Image { get; init; }
        public bool Enterprise { get; init; }
        public bool Orders { get; init; }
        public bool Comments { get; init; }
        public ContactDataLoader(bool details, bool image, bool enterprise, bool orders, bool comments)
        {
            Details = details;
            Image = image;
            Enterprise = enterprise;
            Orders = orders;
            Comments = comments;
        }

        public IQueryable<ContactEntity> LoadData(IQueryable<ContactEntity> query)
        {
            query = Details ? query.Include(contact => contact.Details) : query;
            query = Image ? query.Include(contact => contact.Image) : query;
            query = Enterprise ? query.Include(contact => contact.Enterprise).ThenInclude(enterprise => enterprise.Details) : query;
            if (Orders)
            {
                query = query.Include(contact => contact.ContactOrders)!.ThenInclude(contactorder => contactorder.Order)
                .ThenInclude(order => order.Customer).ThenInclude(customer => customer.Details);
                query = query.Include(contact => contact.ContactOrders)!.ThenInclude(contactorder => contactorder.Order)
                .ThenInclude(order => order.Initiator).ThenInclude(initiator => initiator.Details);
            }
            query = Comments ? query.Include(contact => contact.Comments)!.ThenInclude(comment => comment.Author).ThenInclude(author => author.Details) : query;
            return query;
        }
    }
}
