using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.Core;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMEngSystem.Data.Entities.Comment
{
    public class CommentEntity : IEntity
    {
        public int CommentId { get; set; }

        //Data Properties
        public string Text { get; set; } = default!;
        public DateTime DateTimeCreate { get; set; }

        //Navigation Properties
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual ContactEntity Author { get; set; } = default!;
        public int? RecipientContactId { get; set; }
        [ForeignKey("RecipientContactId")]
        public virtual ContactEntity? RecipientContact { get; set; }
        public int? RecipientEnterpriseId { get; set; }
        public virtual EnterpriseEntity? RecipientEnterprise { get; set; }
        public int? RecipientOrderId { get; set; }
        public virtual OrderEntity? RecipientOrder { get; set; }
    }
}
