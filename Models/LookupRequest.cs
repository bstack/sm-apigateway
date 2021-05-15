using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace apigateway.Models
{
    public class LookupRequest : IValidatableObject
    {
        public Guid LookupId { get; set; }
        public string MerchantId { get; set; }
        public string FromCurrency { get; set; }
        public decimal FromCurrencyValue { get; set; }
        public long CardNumber { get; set; }

        

        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (this.LookupId == Guid.Empty) { yield return new ValidationResult($"{nameof(this.LookupId)} ({this.LookupId}) empty_lookup_id"); }
            if (this.MerchantId == string.Empty) { yield return new ValidationResult($"{nameof(this.MerchantId)} ({this.FromCurrency}) empty_merchant_id"); }
            if (this.FromCurrency == string.Empty) { yield return new ValidationResult($"{nameof(this.FromCurrency)} ({this.FromCurrency}) empty_from_currency"); }
            if (this.FromCurrencyValue <= 0) { yield return new ValidationResult($"{nameof(this.FromCurrencyValue)} ({this.FromCurrencyValue}) from_currency_value_must_be_greater_than_zero"); }
            // Run this in powershell - [System.Math]::Floor([System.Math]::Log10(1234567) + 1)
            if (Math.Floor(Math.Log10(this.CardNumber) + 1) < 13) { yield return new ValidationResult($"{nameof(this.CardNumber)} ({this.CardNumber}) invalid_card_number"); }
        }


        public override string ToString()
        {
            var _cardSuffix = this.CardNumber.ToString().Substring(this.CardNumber.ToString().Length - 4);
            return $"LookupRequest[LookupId: {this.LookupId}, MerchantId: {this.MerchantId}, FromCurrency: {this.FromCurrency}, FromCurrencyValue:{this.FromCurrencyValue}, CardNumber: **********{_cardSuffix}]";
        }
    }
}
