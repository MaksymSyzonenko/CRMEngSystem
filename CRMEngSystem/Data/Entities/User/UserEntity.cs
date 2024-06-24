using CRMEngSystem.Data.Entities.Comment;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.Core;
using CRMEngSystem.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace CRMEngSystem.Data.Entities.User
{
    public class UserEntity : IdentityUser, IEntity
    {
        //Data Properties
        public AccessLevel AccessLevel { get; set; }

        //Navigation Properties
        public int ContactId { get; set; }
        public virtual ContactEntity Contact { get; set; } = null!;
        public virtual ICollection<CommentEntity>? Comments { get; set; }
    }
}
