using CRMEngSystem.Data.Entities.Core;

namespace CRMEngSystem.Data.Entities.Settings
{
    public class SettingsEntity : IEntity
    {
        public int SettingId { get; set; }
        //Data Properties
        public string Key { get; set; } = default!;
        public string Value { get; set; } = default!;
    }
}
