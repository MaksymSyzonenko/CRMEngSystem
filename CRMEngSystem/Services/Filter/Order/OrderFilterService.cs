using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Services.Filter.Core;

namespace CRMEngSystem.Services.Filter.Order
{
    public class OrderFilterService : IFilterService<OrderEntity>
    {
        private readonly string? _filterStatus;
        private readonly string? _filterPriority;
        private readonly DateOnly? _filterDateStart;
        private readonly DateOnly? _filterDateEnd;
        public OrderFilterService(string? filterStatus, string? filterPriority, DateOnly? filterDateStart, DateOnly? filterDateEnd) 
        {
            _filterStatus = filterStatus;
            _filterPriority = filterPriority;
            _filterDateStart = filterDateStart;
            _filterDateEnd = filterDateEnd;
        }

        public IQueryable<OrderEntity> Filter(IQueryable<OrderEntity> entities)
        {
            if (!string.IsNullOrEmpty(_filterStatus) && OrderFilterMap.StatusMap.TryGetValue(_filterStatus, out var status))
                entities = entities.Where(entity => entity.Status == status);

            if (!string.IsNullOrEmpty(_filterPriority) && OrderFilterMap.PriorityMap.TryGetValue(_filterPriority, out var priority))
                entities = entities.Where(entity => entity.Priority == priority);

            if (_filterDateStart.HasValue)
                entities = entities.Where(entity => DateOnly.FromDateTime(entity.DateTimeCreate) >= _filterDateStart.Value);

            if (_filterDateEnd.HasValue)
            {
                var endDate = _filterDateEnd.Value.AddDays(1);
                entities = entities.Where(entity => DateOnly.FromDateTime(entity.DateTimeCreate) < endDate);
            }

            return entities;
        }
    }
}
