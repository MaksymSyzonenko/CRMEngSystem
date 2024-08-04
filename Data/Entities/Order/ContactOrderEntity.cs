using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.Core;

namespace CRMEngSystem.Data.Entities.Order
{
    public class ContactOrderEntity : IEntity
    {
        //Data Properties
        public bool IsOfferRecipient { get; set; }

        //Navigation Properties
        public int ContactId { get; set; }
        public int OrderId { get; set; }
        public virtual ContactEntity Contact { get; set; } = null!;
        public virtual OrderEntity Order { get; set; } = null!;
    }
}
