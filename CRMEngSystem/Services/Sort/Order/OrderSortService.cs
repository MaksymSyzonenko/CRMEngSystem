using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Services.Sort.Core;

namespace CRMEngSystem.Services.Sort.Order
{
    public class OrderSortService : ISortService<OrderEntity>
    {
        private readonly bool? _sortOrderId;
        private readonly bool? _sortAlphabetCustomerName;
        private readonly bool? _sortPriority;
        private readonly bool? _sortDateTimeCreate;
        public OrderSortService(bool? sortOrderId, bool? sortAlphabetCustomerName, bool? sortPriority, bool? sortDateTimeCreate)
        {
            _sortOrderId = sortOrderId;
            _sortAlphabetCustomerName = sortAlphabetCustomerName;
            _sortPriority = sortPriority;
            _sortDateTimeCreate = sortDateTimeCreate;
        }

        public IQueryable<OrderEntity> Sort(IQueryable<OrderEntity> entities)
        {
            if(!_sortOrderId.HasValue && !_sortAlphabetCustomerName.HasValue && !_sortPriority.HasValue && !_sortDateTimeCreate.HasValue)
                entities = entities.OrderByDescending(entity => entity.DateTimeCreate);

            if (_sortOrderId.HasValue)
                entities = _sortOrderId.Value ? entities.OrderBy(entity => entity.OrderId) : entities;

            if (_sortAlphabetCustomerName.HasValue)
                entities = _sortAlphabetCustomerName.Value ? entities.OrderBy(entity => entity.Customer.Details.NameUA) : entities;

            if (_sortPriority.HasValue)
                entities = _sortPriority.Value ? entities.OrderBy(entity => entity.Priority) : entities;

            if(_sortDateTimeCreate.HasValue)
                entities = _sortDateTimeCreate.Value ? entities.OrderByDescending(entity => entity.DateTimeCreate) : entities;

            return entities;
        }
    }
}
