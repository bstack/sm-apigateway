using System;
using System.Threading.Tasks;

namespace apigateway.External.Rates
{
	public interface IClient
    {
        Task<(RateStatus RateStatus, RateResponse RateResponse)> Lookup(
            string correlationId,
            string fromCurrency,
            decimal fromCurrencyValue,
            string toCurrency);
    }
}