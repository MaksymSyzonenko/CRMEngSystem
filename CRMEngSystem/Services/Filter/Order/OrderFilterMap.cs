using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Enums;

namespace CRMEngSystem.Services.Filter.Order
{
    public static class OrderFilterMap
    {
        public static Dictionary<string, OrderStatusValue> StatusMap = new()
        {
            { "Processing", OrderStatusValue.Processing },
            { "Offer", OrderStatusValue.Offer },
            { "Execution", OrderStatusValue.Execution },
            { "Completed", OrderStatusValue.Completed },
            { "Обробка", OrderStatusValue.Processing },
            { "Пропозиція", OrderStatusValue.Offer },
            { "Виконання", OrderStatusValue.Execution },
            { "Завершене", OrderStatusValue.Completed }
        };

        public static Dictionary<string, PriorityValue> PriorityMap = new()
        {
            { "Низький", PriorityValue.Low },
            { "Середній", PriorityValue.Medium },
            { "Високий", PriorityValue.High }
        };
    }
}
