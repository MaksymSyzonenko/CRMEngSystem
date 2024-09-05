namespace CRMEngSystem.Services.Calculate
{
    public static class PriceCalculator
    {
        public static decimal CalculatePurchasePrice(int discount, decimal price)
            => (1 - (discount / 100.0m)) * price;

        public static decimal CalculateSellPrice(int discount, int markUp, decimal price)
            => (1 + (markUp / 100.0m)) * CalculatePurchasePrice(discount, price);

        public static decimal CalculateShippingCost(decimal weight, decimal volume, decimal weightRate, decimal volumeRate)
            => (weight > 1 ? weight : 1) * weightRate + volume * volumeRate;
    }
}
