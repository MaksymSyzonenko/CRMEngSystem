
using CRMEngSystem.Data.Entities.Core;

namespace CRMEngSystem.Data.Entities.Contact
{
    public class ContactImageEntity : IEntity
    {
        public int ContactImageId { get; set; }

        //Data Properties
        public byte[]? Bytes { get; set; }

        //Navigation Properties
        public int ContactId { get; set; }
        public virtual ContactEntity Contact { get; set; } = null!;

    }
}
