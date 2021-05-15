using System;
using System.Threading.Tasks;

namespace apigateway.External.BINs
{
	public interface IClient
    {
        Task<(LookupStatus LookupStatus, LookupResponse LookupResponse)> Lookup(
            string correlationId,
            string merchantId,
            string cardNumberBIN);
    }
}