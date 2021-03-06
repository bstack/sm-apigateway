using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace apigateway.External.Reporting
{
	public class Client : IClient
    {
        private readonly HttpClient c_httpClient;
        private readonly string c_requestUri;


        public Client(
            AppSettings appSettings)
        {
            this.c_httpClient = new HttpClient();
            this.c_requestUri = appSettings.ReportingRequestUri;
        }


        public async void LogActivity(
            string requestId,
            string correlationId,
            string activity,
            string activityDetail)
        {
            var _logActivityRequest = new LogActivityRequest(activity, activityDetail);
            var _logActivityRequestAsString = JsonConvert.SerializeObject(_logActivityRequest);
            Console.WriteLine(_logActivityRequestAsString);
            var _content = new StringContent(_logActivityRequestAsString);
            _content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _content.Headers.Add("X-Request-Id", requestId);
            _content.Headers.Add("X-Correlation-Id", correlationId);
            await this.c_httpClient.PostAsync(this.c_requestUri, _content);
            return;
        }

        public async Task<IEnumerable<string>> Get()
        {
            var httpResponse = await this.c_httpClient.GetAsync(this.c_requestUri);
            return JsonConvert.DeserializeObject<IEnumerable<string>>(httpResponse.Content.ReadAsStringAsync().Result);
        }
    }
}