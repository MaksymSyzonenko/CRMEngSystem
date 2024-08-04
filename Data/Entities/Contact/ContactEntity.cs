using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.Comment;
using CRMEngSystem.Data.Entities.Core;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Entities.User;

namespace CRMEngSystem.Data.Entities.Contact
{
    public class ContactEntity : IEntity
    {
        public int ContactId { get; set; }

        //Data Properties
        public DateTime DateTimeCreate { get; set; }
        public DateTime? DateTimeUpdate { get; set; }

        //Navigation Properties
        public virtual ContactDetailsEntity Details { get; set; } = null!;
        public virtual UserEntity? User { get; set; }
        public virtual ContactImageEntity? Image { get; set; }
        public int EnterpriseId { get; set; }
        public virtual EnterpriseEntity Enterprise { get; set; } = null!;
        public virtual ICollection<CommentEntity>? Comments { get; set; }
        public virtual ICollection<ContactOrderEntity>? ContactOrders { get; set; }
    }
}
