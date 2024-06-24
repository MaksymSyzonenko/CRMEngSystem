using CRMEngSystem.Data.Entities.Comment;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.Core;
using CRMEngSystem.Data.Entities.Order;

namespace CRMEngSystem.Data.Entities.Enterprise
{
    public class EnterpriseEntity : IEntity
    {
        public int EnterpriseId { get; set; }

        //Data Properties
        public DateTime DateTimeCreate { get; set; }
        public DateTime? DateTimeUpdate { get; set; }

        //Navigation Properties
        public virtual EnterpriseDetailsEntity Details { get; set; } = null!;
        public virtual ICollection<ContactEntity>? Contacts { get; set; }
        public virtual ICollection<CommentEntity>? Comments { get; set; }
        public virtual ICollection<OrderEntity>? Orders { get; set; }
    }
}
