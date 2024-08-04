using CRMEngSystem.Data.Enums;

namespace CRMEngSystem.Services.Filter.Catalog
{
    public static class CatalogFilterMap
    {
        public static Dictionary<string, EquipmentTypeValue> TypeMap = new()
        {
            { "Основне", EquipmentTypeValue.Main },
            { "Деталь", EquipmentTypeValue.Detail },
            { "Ремкомплект", EquipmentTypeValue.MaintenanceKit }
        };
    }
}
