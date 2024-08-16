using CRMEngSystem.Data.Enums;

namespace CRMEngSystem.Models.ViewModels.Order
{
    public sealed class OrderStatusViewModel
    {
        public int OrderId { get; set; }
        public OrderStatusValue Status { get; set; }
        public DateTime DateTimeProcessingStatusStart { get; set; }
        public DateTime? DateTimeOfferStatusStart { get; set; }
        public DateTime? DateTimeExecutionStatusStart { get; set; }
        public DateTime? DateTimeCompletedStatusStart { get; set; }
    }
}
