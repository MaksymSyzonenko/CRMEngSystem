namespace CRMEngSystem.Configuration
{
    public static class ConfigurationSettings
    {
        public static decimal CurrencyCoefficient { get; set; }
        public static decimal CurrencyCoefficient_UAH_EUR { get; set; }
        public static decimal ShippingRatePerKg { get; set; }
        public static decimal ShippingRatePerCubicMeter { get; set; }
        public static string WordTemplate { get; set; } = default!;
        public static string ExcelTemplate { get; set; } = default!;
        public static string WareHouseExample { get; set; } = default!;
        public static string UpdatePricesExample { get; set; } = default!;
        public static string Manual { get; set; } = default!;
    }
}
