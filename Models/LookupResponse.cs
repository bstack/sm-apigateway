using System;

namespace apigateway.Models
{
	public class LookupResponse
	{
		public Guid LookupId { get; set; }
		public string MerchantId { get; set; }
		public string FromCurrency { get; set; }
		public decimal FromCurrencyValue { get; set; }
		public string CardScheme { get; set; }
		public string CardCountry { get; set; }
		public string CardCurrency { get; set; }
		public decimal CardCurrencyValue { get; set; }
		public decimal CardCurrencyMarginValue { get; set; }
		public decimal RateApplied { get; set; }
		public decimal MarginPercentage { get; set; }
		

		public LookupResponse(
			Guid lookupId,
			string merchantId,
			string fromCurrency,
			decimal fromCurrencyValue,
			string cardScheme,
			string cardCountry,
			string cardCurrency,
			decimal cardCurrencyValue,
			decimal cardCurrencyMarginValue,
			decimal rateApplied,
			decimal marginPercentage)
		{
			this.LookupId = lookupId;
			this.MerchantId = merchantId;
			this.FromCurrency = fromCurrency;
			this.FromCurrencyValue = fromCurrencyValue;
			this.CardScheme = cardScheme;
			this.CardCountry = cardCountry;
			this.CardCurrency = cardCurrency;
			this.CardCurrencyValue = cardCurrencyValue;
			this.CardCurrencyMarginValue = cardCurrencyMarginValue;
			this.RateApplied = rateApplied;
			this.MarginPercentage = marginPercentage;

		}

		public override string ToString()
		{
			return $"LookupResponse[LookupId: {this.LookupId}, MerchantId: {this.MerchantId}, FromCurrency: {this.FromCurrency}, FromCurrencyValue: {this.FromCurrencyValue}, CardScheme:{this.CardScheme}, CardCountry:{this.CardCountry}, CardCurrency:{this.CardCurrency}, CardCurrencyValue:{this.CardCurrencyValue}, CardCurrencyMarginValue:{this.CardCurrencyMarginValue}, RateApplied:{this.RateApplied}, MarginPercentage:{this.MarginPercentage}]";
		}
	}
}
