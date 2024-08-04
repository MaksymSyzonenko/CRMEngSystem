using CRMEngSystem.Data.Entities.Comment;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.Core;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Enums;

namespace CRMEngSystem.Data.Entities.Order
{
    public class OrderEntity : IEntity
    {
        public int OrderId { get; set; }

        //Data Properties
        public OrderStatusValue Status { get; set; }
        public DateTime DateTimeProcessingStatusStart { get; set; }
        public DateTime? DateTimeOfferStatusStart { get; set; }
        public DateTime? DateTimeExecutionStatusStart { get; set; }
        public DateTime? DateTimeCompletedStatusStart { get; set; }
        public PriorityValue Priority { get; set; }
        public DateTime DateTimeCreate { get; set; }
        public DateTime? DateTimeUpdate { get; set; }
        
        //Navigation Properties
        public int CustomerId { get; set; }
        public virtual EnterpriseEntity Customer { get; set; } = null!;
        public int InitiatorId { get; set; }
        public virtual ContactEntity Initiator { get; set; } = null!;
        public virtual ICollection<EquipmentOrderPositionEntity>? EquipmentOrderPositions { get; set; }
        public virtual ICollection<ContactOrderEntity> ContactOrders { get; set; } = null!;
        public virtual ICollection<CommentEntity>? Comments { get; set; }
    }
}
