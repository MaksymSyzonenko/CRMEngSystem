using Microsoft.EntityFrameworkCore;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Loaders.Core;

namespace CRMEngSystem.Data.Loaders.Order
{
    public sealed record OrderDataLoader : IEntityDataLoader<OrderEntity>
    {
        public bool Customer { get; init; }
        public bool Initiator { get; init; }
        public bool Contacts { get; init; }
        public bool Comments { get; init; }
        public bool EquipmentOrderPositions { get; init; }
        public OrderDataLoader(bool customer, bool initiator, bool contacts, bool comments, bool equipmentOrderPositions)
        {
            Customer = customer;
            Initiator = initiator;
            Contacts = contacts;
            Comments = comments;
            EquipmentOrderPositions = equipmentOrderPositions;
        }

        public IQueryable<OrderEntity> LoadData(IQueryable<OrderEntity> query)
        {
            query = Customer ? query.Include(order => order.Customer).ThenInclude(enterprise => enterprise.Details) : query;
            query = Customer ? query.Include(order => order.Initiator).ThenInclude(contact => contact.Details) : query;
            query = Contacts ? query.Include(order => order.ContactOrders).ThenInclude(contactorder => contactorder.Contact).ThenInclude(contact => contact.Details) : query;
            query = Contacts ? query.Include(order => order.ContactOrders).ThenInclude(contactorder => contactorder.Contact).ThenInclude(contact => contact.Image) : query;
            query = Contacts ? query.Include(order => order.ContactOrders).ThenInclude(contactorder => contactorder.Contact).ThenInclude(contact => contact.Enterprise).ThenInclude(enterprise => enterprise.Details) : query;
            query = Comments ? query.Include(contact => contact.Comments)!.ThenInclude(comment => comment.Author).ThenInclude(author => author.Details) : query;
            query = EquipmentOrderPositions ? query.Include(order => order.EquipmentOrderPositions)!.ThenInclude(position => position.EquipmentCatalogPosition) : query;
            return query;
        }
    }
}
