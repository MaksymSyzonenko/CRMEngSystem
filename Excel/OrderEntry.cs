namespace CRMEngSystem.Excel
{
    public sealed class OrderEntry
    {
        public string PartNumber { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public decimal Price { get; set; }
        public string Customer { get; set; } = default!;
        public string TradeGroup { get; set; } = default!;
        public string OrderNumber { get; set; } = default!;
    }
}
