using System;


namespace apigateway.External.Rates
{
    public class RateResponse
    {
		public string FromCurrency  { get; set; }
		public decimal FromCurrencyValue  { get; set; }
		public decimal RateApplied  { get; set; }
		public decimal MarginPercentage  { get; set; }
		public string ToCurrency  { get; set; }
		public decimal ToCurrencyValue  { get; set; }
		public decimal ToCurrencyMarginValue  { get; set; }


		// Required for serialization purposes
		public RateResponse()
        {

        }


		public RateResponse(
			string fromCurrency,
			decimal fromCurrencyValue,
			decimal rateApplied,
			decimal marginPercentage,
			string toCurrency,
			decimal toCurrencyValue,
			decimal toCurrencyMarginValue)
		{
			this.FromCurrency = fromCurrency;
			this.FromCurrencyValue = fromCurrencyValue;
			this.RateApplied = rateApplied;
			this.MarginPercentage = marginPercentage;
			this.ToCurrency = toCurrency;
			this.ToCurrencyValue = toCurrencyValue;
			this.ToCurrencyMarginValue = toCurrencyMarginValue;
		}
	}
}