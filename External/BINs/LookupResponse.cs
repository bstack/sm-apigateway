using System;


namespace apigateway.External.BINs
{
    public class LookupResponse
    {
		public string CardScheme { get; set; }
		public string Country { get; set; }
		public string Currency { get; set; }

		// Required for serialization purposes
        public LookupResponse()
        {

        }


		public LookupResponse(
			string cardScheme,
			string country,
			string currency)
		{
			this.CardScheme = cardScheme;
			this.Country = country;
			this.Currency = currency;
		}
	}
}