using System.Net.Http.Json;

namespace PositionSizeCalculator.Utilities
{
    public class ExchangeRateProvider
    {
        private static readonly HttpClient httpClient = new();

        private class ExchangeRateResponse
        {
            public Dictionary<string, decimal> ExchangeRates { get; set; }
        }

        public async Task<decimal> ConvertAsync(decimal amount, string from, string to)
        {
            return await GetExchangeRateAsync(from, to) * amount;
        }

        public async Task<decimal> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            var url = $"https://api.exchangerate.host/latest?base={fromCurrency}&symbols={toCurrency}";
            var response = await httpClient.GetFromJsonAsync<ExchangeRateResponse>(url);

            if (response?.ExchangeRates != null && response.ExchangeRates.TryGetValue(toCurrency, out var rate))
            {
                return rate;
            }

            throw new Exception("Failed to retrieve USD to SEK exchange rate.");
        }
    }
}
