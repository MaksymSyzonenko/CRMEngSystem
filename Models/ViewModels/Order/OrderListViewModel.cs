using CRMEngSystem.Dto.Order;
using CRMEngSystem.Models.ViewModels.Pagination;

namespace CRMEngSystem.Models.ViewModels.Order
{
    public sealed class OrderListViewModel : PaginationViewModel
    {
        public IEnumerable<OrderListItemDto> Entities { get; set; } = default!;
        public string? SearchOrderId { get; init; }
        public string? SearchCustomerName { get; init; }
        public string? FilterPriority { get; init; }
        public string? SearchInitiatorInitials { get; init; }
        public DateOnly? FilterDateStart { get; init; }
        public DateOnly? FilterDateEnd { get; init; }
        public string FilterStatus { get; init; } = "Processing";
        public bool? SortOrderId { get; init; }
        public bool? SortAlphabetCustomerName { get; init; }
        public bool? SortPriority { get; init; }
        public bool? SortDateTimeCreate { get; init; }
        public int? NumberItemsPerProcessing { get; set; }
        public int? NumberItemsPerOffer { get; set; }
        public int? NumberItemsPerExecution { get; set; }
        public int? NumberItemsPerCompleted { get; set; }
        public int? NumberItemsPerAll { get; set; }
    }
}
