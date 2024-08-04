using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Loaders.Core;

namespace CRMEngSystem.Data.Loaders.Enterprise
{
    public sealed record EnterpriseDataLoader : IEntityDataLoader<EnterpriseEntity>
    {
        public bool Details { get; init; }
        public bool Orders { get; init; }
        public bool Contacts { get; init; }
        public bool Comments { get; init; }
        public EnterpriseDataLoader(bool details, bool orders, bool contacts, bool comments)
        {
            Details = details;
            Orders = orders;
            Contacts = contacts;
            Comments = comments;
        }

        public IQueryable<EnterpriseEntity> LoadData(IQueryable<EnterpriseEntity> query)
        {
            query = Details ? query.Include(enterprise => enterprise.Details) : query;
            query = Orders ? query.Include(enterprise => enterprise.Orders)!.ThenInclude(order => order.EquipmentOrderPositions) : query;
            query = Orders ? query.Include(enterprise => enterprise.Orders)!.ThenInclude(order => order.Initiator).ThenInclude(initiator => initiator.Details) : query;
            query = Contacts ? query.Include(enterprise => enterprise.Contacts)!.ThenInclude(contact => contact.Details) : query;
            query = Contacts ? query.Include(enterprise => enterprise.Contacts)!.ThenInclude(contact => contact.Image) : query;
            query = Comments ? query.Include(contact => contact.Comments)!.ThenInclude(comment => comment.Author).ThenInclude(author => author.Details) : query;
            return query;
        }
    }
}
