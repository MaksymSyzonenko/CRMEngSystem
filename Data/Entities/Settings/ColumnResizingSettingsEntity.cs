using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Entities.Core;

namespace CRMEngSystem.Data.Entities.Settings
{
    public class ColumnResizeSettingsEntity : IEntity
    {
        public int ColumnResizeSettingsId { get; set; }
        //Data Properties
        public string PageIdentifier { get; set; } = default!;
        public string ColumnSizes { get; set; } = default!;
        //Navigation Properties
        public string UserId { get; set; } = default!;
        public virtual UserEntity User { get; set; } = default!;
    }

}
