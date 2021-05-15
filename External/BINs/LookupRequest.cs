using System;


namespace apigateway.External.BINs
{
    public class LookupRequest
    {
        public string MerchantId { get; set; }
        public long CardNumberBin { get; set; }


        public LookupRequest(
            string merchantId,
            long cardNumberBin)
        {
            this.MerchantId = merchantId;
            this.CardNumberBin = cardNumberBin;
        }
    }
}