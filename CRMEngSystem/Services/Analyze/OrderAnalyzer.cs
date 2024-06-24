using CRMEngSystem.Data.Entities.Order;
using System.Globalization;

namespace CRMEngSystem.Services.Analyze
{
    public sealed class OrderAnalyzer
    {
        private readonly List<OrderEntity>? _orders;

        public OrderAnalyzer(List<OrderEntity>? orders)
        {
            _orders = orders;
        }

        public (List<string> months, List<int> orderCounts, List<decimal> totalOrderAmounts) AnalyzeLast12Months()
        {
            if (_orders == null)
                return  (new List<string>(), new List<int>(), new List<decimal>());

            var now = DateTime.Now;
            var nextMonth = now.AddMonths(1);
            var last12Months = Enumerable.Range(1, 12)
                .Select(i => nextMonth.AddMonths(-i))
                .Reverse()
                .ToList();

            var months = new List<string>();
            var orderCounts = new List<int>();
            var totalOrderAmounts = new List<decimal>();

            foreach (var month in last12Months)
            {
                var monthStart = new DateTime(month.Year, month.Month, 1);
                var monthEnd = monthStart.AddMonths(1);

                var ordersInMonth = _orders
                    .Where(o => o.DateTimeCreate >= monthStart && o.DateTimeCreate < monthEnd)
                    .ToList();

                var orderCount = ordersInMonth.Count;
                var totalOrderAmount = ordersInMonth.Sum(CalculateOrderAmount);

                months.Add(month.ToString("MMMM", CultureInfo.CreateSpecificCulture("uk-UA")));
                orderCounts.Add(orderCount);
                totalOrderAmounts.Add(totalOrderAmount);
            }

            return (months, orderCounts, totalOrderAmounts);
        }

        private decimal CalculateOrderAmount(OrderEntity order)
        {
            return order.EquipmentOrderPositions!.Sum(position => position.SellPrice * position.Quantity);
        }
    }



}
