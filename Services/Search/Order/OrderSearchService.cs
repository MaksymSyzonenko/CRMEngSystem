using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Services.Search.Core;

namespace CRMEngSystem.Services.Search.Order
{
    public sealed class OrderSearchService : ISearchService<OrderEntity>
    {
        private readonly string? _searchOrderId;
        private readonly string? _searchCustomerName;
        private readonly string? _searchSearchInitiatorInitials;
        public OrderSearchService(string? searchOrderId, string? searchCustomerName, string? searchSearchInitiatorInitials)
        {
            _searchOrderId = searchOrderId;
            _searchCustomerName = searchCustomerName;
            _searchSearchInitiatorInitials = searchSearchInitiatorInitials;
        }
        
        public IQueryable<OrderEntity> Search(IQueryable<OrderEntity> entities)
        {
            entities = entities.Where(entity =>
                 (string.IsNullOrEmpty(_searchOrderId) ||
                     entity.OrderId.ToString() == _searchOrderId) &&
                 (string.IsNullOrEmpty(_searchCustomerName) ||
                     (entity.Customer.Details.NameUA != null && entity.Customer.Details.NameUA.ToLower().Contains(_searchCustomerName.ToLower())) ||
                     (entity.Customer.Details.NameEN != null && entity.Customer.Details.NameEN.ToLower().Contains(_searchCustomerName.ToLower()))) &&
                 (string.IsNullOrEmpty(_searchSearchInitiatorInitials) ||
                     (entity.Initiator.Details.FirstName != null && entity.Initiator.Details.FirstName.ToLower().Contains(_searchSearchInitiatorInitials.ToLower())) ||
                     (entity.Initiator.Details.LastName != null && entity.Initiator.Details.LastName.ToLower().Contains(_searchSearchInitiatorInitials.ToLower())) ||
                     (entity.Initiator.Details.MiddleName != null && entity.Initiator.Details.MiddleName.ToLower().Contains(_searchSearchInitiatorInitials.ToLower())) ||
                     (entity.Initiator.Details.FirstName != null && entity.Initiator.Details.LastName != null && entity.Initiator.Details.MiddleName != null &&
                     _searchSearchInitiatorInitials.ToLower() == (entity.Initiator.Details.LastName.Substring(0, 1).ToLower() + entity.Initiator.Details.FirstName.Substring(0, 1).ToLower() + entity.Initiator.Details.MiddleName.Substring(0, 1).ToLower()))));

            return entities;
        }

        public IEnumerable<OrderEntity> SearchGeneral(IEnumerable<OrderEntity> entities)
        {
            entities = entities.Where(entity =>
                 (string.IsNullOrEmpty(_searchOrderId) ||
                     entity.OrderId.ToString() == _searchOrderId) ||
                 (string.IsNullOrEmpty(_searchCustomerName) ||
                     (entity.Customer.Details.NameUA != null && entity.Customer.Details.NameUA.ToLower().Contains(_searchCustomerName.ToLower())) ||
                     (entity.Customer.Details.NameEN != null && entity.Customer.Details.NameEN.ToLower().Contains(_searchCustomerName.ToLower()))) ||
                 (string.IsNullOrEmpty(_searchSearchInitiatorInitials) ||
                     (entity.Initiator.Details.FirstName != null && entity.Initiator.Details.FirstName.ToLower().Contains(_searchSearchInitiatorInitials.ToLower())) ||
                     (entity.Initiator.Details.LastName != null && entity.Initiator.Details.LastName.ToLower().Contains(_searchSearchInitiatorInitials.ToLower())) ||
                     (entity.Initiator.Details.MiddleName != null && entity.Initiator.Details.MiddleName.ToLower().Contains(_searchSearchInitiatorInitials.ToLower())) ||
                     (entity.Initiator.Details.FirstName != null && entity.Initiator.Details.LastName != null && entity.Initiator.Details.MiddleName != null &&
                     _searchSearchInitiatorInitials.ToLower() == (entity.Initiator.Details.LastName.Substring(0, 1).ToLower() + entity.Initiator.Details.FirstName.Substring(0, 1).ToLower() + entity.Initiator.Details.MiddleName.Substring(0, 1).ToLower()))));

            return entities;
        }
    }
}
