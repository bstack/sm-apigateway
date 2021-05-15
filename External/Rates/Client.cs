using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace apigateway.External.Rates
{
	public class Client : IClient
    {
        private readonly HttpClient c_httpClient;
        private readonly string c_requestUri;


        public Client(
            AppSettings appSettings)
        {
            this.c_httpClient = new HttpClient();
            this.c_requestUri = appSettings.RatesRequestUri;
        }


        public async Task<(RateStatus RateStatus, RateResponse RateResponse)> Lookup(
            string correlationId,
            string fromCurrency,
            decimal fromCurrencyValue,
            string toCurrency)
        {
            var _rateRequest = new RateRequest(fromCurrency, fromCurrencyValue, toCurrency);
            var _rateRequestAsString = JsonConvert.SerializeObject(_rateRequest);
            Console.WriteLine(_rateRequestAsString);
            var _content = new StringContent(_rateRequestAsString);
            _content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _content.Headers.Add("X-Correlation-Id", correlationId);

            var _httpResponse = await this.c_httpClient.PostAsync(this.c_requestUri, _content);

            var _lookupStatus = this.MapLookupStatus(_httpResponse.StatusCode);
            Console.WriteLine(_httpResponse.StatusCode);
            var _lookupResponse = JsonConvert.DeserializeObject<RateResponse>(_httpResponse.Content.ReadAsStringAsync().Result);
            return (_lookupStatus, _lookupResponse);
        }

        private RateStatus MapLookupStatus(
            HttpStatusCode statusCode)
        {
            switch(statusCode)
            {
                case HttpStatusCode.Created:
                    return RateStatus.Success;
                default:
                    return RateStatus.RateServiceServerError;
            }
        }
    }
}