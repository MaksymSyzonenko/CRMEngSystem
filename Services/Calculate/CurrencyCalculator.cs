using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;

namespace CRMEngSystem.Services.Calculate
{
    public sealed class CurrencyCalculator
    {
        private readonly IMemoryCache _cache;
        private const string CacheKey = "CurrencyRates";
        private static readonly TimeSpan CacheDuration = TimeSpan.FromDays(1);

        public decimal UAH_GBP_Currency { get; private set; }
        public decimal UAH_EUR_Currency { get; private set; }
        public decimal UAH_USD_Currency { get; private set; }
        public decimal UAH_EUR_RateChanges { get; private set; }
        public decimal UAH_USD_RateChanges { get; private set; }
        public decimal UAH_GBP_RateChanges { get; private set; }
        public decimal GBP_EUR_Currency { get; private set; }
        public decimal GBP_USD_Currency { get; private set; }
        public decimal EUR_GBP_Currency { get; private set; }
        public decimal EUR_USD_Currency { get; private set; }
        public decimal USD_GBP_Currency { get; private set; }
        public decimal USD_EUR_Currency { get; private set; }

        public CurrencyCalculator(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task Initialize()
        {
            if (!_cache.TryGetValue(CacheKey, out CurrencyCalculator cachedCalculator))
            {
                await UpdateRates();
                _cache.Set(CacheKey, this, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = CacheDuration
                });
            }
            else
            {
                CopyFrom(cachedCalculator);
            }
        }

        private async Task UpdateRates()
        {
            string[] currencies = { "USD", "EUR", "GBP" };
            string apiUrl = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
            var currentRates = await GetExchangeRates(apiUrl);

            var timeAgo = DateTime.Now.AddDays(-30).ToString("yyyyMMdd");
            var timeAgoRates = await GetExchangeRates(apiUrl, timeAgo);

            var rateChanges = new Dictionary<string, decimal>();
            foreach (var currency in currencies)
            {
                decimal currentRate = currentRates[currency];
                decimal previousRate = timeAgoRates[currency];
                decimal percentageChange = ((currentRate - previousRate) / previousRate) * 100;
                rateChanges[currency] = percentageChange;
            }

            var crossRates = new Dictionary<string, decimal>();
            foreach (var baseCurrency in currencies)
            {
                foreach (var quoteCurrency in currencies)
                {
                    if (baseCurrency != quoteCurrency)
                    {
                        decimal baseRateToUAH = currentRates[baseCurrency];
                        decimal quoteRateToUAH = currentRates[quoteCurrency];
                        decimal exchangeRate = baseRateToUAH / quoteRateToUAH;
                        crossRates[$"{baseCurrency}_{quoteCurrency}"] = exchangeRate;
                    }
                }
            }

            UAH_GBP_Currency = currentRates["GBP"];
            UAH_EUR_Currency = currentRates["EUR"];
            UAH_USD_Currency = currentRates["USD"];
            UAH_GBP_RateChanges = rateChanges["GBP"];
            UAH_EUR_RateChanges = rateChanges["EUR"];
            UAH_USD_RateChanges = rateChanges["USD"];
            GBP_EUR_Currency = crossRates["GBP_EUR"];
            GBP_USD_Currency = crossRates["GBP_USD"];
            EUR_GBP_Currency = crossRates["EUR_GBP"];
            EUR_USD_Currency = crossRates["EUR_USD"];
            USD_GBP_Currency = crossRates["USD_GBP"];
            USD_EUR_Currency = crossRates["USD_EUR"];
        }

        private async Task<Dictionary<string, decimal>> GetExchangeRates(string apiUrl, string date = null)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = apiUrl;
                if (date != null)
                {
                    url += $"&date={date}";
                }

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                JArray data = JArray.Parse(responseBody);

                var rates = new Dictionary<string, decimal>();
                foreach (var item in data)
                {
                    string currency = item["cc"].ToString();
                    decimal rate = (decimal)item["rate"];
                    rates[currency] = rate;
                }
                return rates;
            }
        }

        private void CopyFrom(CurrencyCalculator other)
        {
            UAH_GBP_Currency = other.UAH_GBP_Currency;
            UAH_EUR_Currency = other.UAH_EUR_Currency;
            UAH_USD_Currency = other.UAH_USD_Currency;
            UAH_GBP_RateChanges = other.UAH_GBP_RateChanges;
            UAH_EUR_RateChanges = other.UAH_EUR_RateChanges;
            UAH_USD_RateChanges = other.UAH_USD_RateChanges;
            GBP_EUR_Currency = other.GBP_EUR_Currency;
            GBP_USD_Currency = other.GBP_USD_Currency;
            EUR_GBP_Currency = other.EUR_GBP_Currency;
            EUR_USD_Currency = other.EUR_USD_Currency;
            USD_GBP_Currency = other.USD_GBP_Currency;
            USD_EUR_Currency = other.USD_EUR_Currency;
        }
    }

}
