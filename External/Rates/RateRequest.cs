using System;


namespace apigateway.External.Rates
{
    public class RateRequest
    {
        public string FromCurrency { get; set; }
        public decimal FromCurrencyValue { get; set; }
        public string ToCurrency { get; set; }


        public RateRequest(
            string fromCurrency,
            decimal fromCurrencyValue,
            string toCurrency)
        {
            this.FromCurrency = fromCurrency;
            this.FromCurrencyValue = fromCurrencyValue;
            this.ToCurrency = toCurrency;
        }
    }
}