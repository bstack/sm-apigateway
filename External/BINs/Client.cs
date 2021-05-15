using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace apigateway.External.BINs
{
	public class Client : IClient
    {
        private readonly HttpClient c_httpClient;
        private readonly string c_requestUri;


        public Client(
            AppSettings appSettings)
        {
            this.c_httpClient = new HttpClient();
            this.c_requestUri = appSettings.BINsRequestUri;
        }


        public async Task<(LookupStatus, LookupResponse)> Lookup(
            string correlationId,
            string merchantId,
            string cardNumberBIN)
        {
            var _lookupRequest = new LookupRequest(merchantId, Convert.ToInt64(cardNumberBIN));
            var _lookupRequestAsString = JsonConvert.SerializeObject(_lookupRequest);
            var _content = new StringContent(_lookupRequestAsString);
            _content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _content.Headers.Add("X-Correlation-Id", correlationId);

            var _httpResponse = await this.c_httpClient.PostAsync(this.c_requestUri, _content);

            var _lookupStatus = this.MapLookupStatus(_httpResponse.StatusCode);
            var _lookupResponse = JsonConvert.DeserializeObject<LookupResponse>(_httpResponse.Content.ReadAsStringAsync().Result);
            return (_lookupStatus, _lookupResponse);
        }


        private LookupStatus MapLookupStatus(
            HttpStatusCode statusCode)
        {
            switch(statusCode)
            {
                case HttpStatusCode.Created:
                case HttpStatusCode.OK:
                    return LookupStatus.Success;
                case HttpStatusCode.NotFound:
                    return LookupStatus.NoMatchFound;
                default:
                    return LookupStatus.BINServiceServerError;
            }
        }
    }
}