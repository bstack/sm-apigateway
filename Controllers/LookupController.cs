using apigateway.External.BINs;
using apigateway.External.Rates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace apigateway.Controllers
{
    [Route("api/[controller]")]
    public class LookupController : ControllerBase
    {
        private readonly External.BINs.IClient c_binsClient;
        private readonly External.Rates.IClient c_ratesClient; 
        private readonly External.Reporting.IClient c_reportingClient;


        public LookupController(
            [FromServices] External.BINs.IClient binsClient,
            [FromServices] External.Rates.IClient ratesClient,
            [FromServices] External.Reporting.Client reportingClient)
        {
            this.c_binsClient = binsClient;
            this.c_ratesClient = ratesClient;
            this.c_reportingClient = reportingClient;
        }


        [HttpPost]
        public IActionResult Post(
            [FromBody] Models.LookupRequest lookupRequest)
        {
            var _requestId = Guid.NewGuid().ToString();
            var _correlationId = lookupRequest.LookupId.ToString();
            this.c_reportingClient.LogActivity(_requestId, _correlationId, "APIGatewayController.Post", "Start");

            this.c_reportingClient.LogActivity(_requestId, _correlationId, "APIGatewayController.Post", "Calling external BINs request");
            var _cardBIN = lookupRequest.CardNumber.ToString().Substring(0, 6);
            var _binClientResponse = this.c_binsClient.Lookup(_correlationId, lookupRequest.MerchantId, _cardBIN).Result;

            if(_binClientResponse.LookupStatus == LookupStatus.NoMatchFound)
            {
                this.c_reportingClient.LogActivity(_requestId, _correlationId, "APIGatewayController.Post", $"Received external BINs error response: Value:{_binClientResponse.LookupStatus}");
                return this.StatusCode(StatusCodes.Status500InternalServerError, "bin_lookup_failure");
            }
            else if (_binClientResponse.LookupStatus == LookupStatus.BINServiceServerError)
            {
                this.c_reportingClient.LogActivity(_requestId, _correlationId, "APIGatewayController.Post", $"Received external BINs error response: Value:{_binClientResponse.LookupStatus}");
                return this.StatusCode(StatusCodes.Status500InternalServerError, "bin_lookup_service_error");
            }
            else
            {
                this.c_reportingClient.LogActivity(_requestId, _correlationId, "APIGatewayController.Post", $"Received external BINs success response: Value:{_binClientResponse.LookupResponse}");
            }

            this.c_reportingClient.LogActivity(_requestId, _correlationId, "APIGatewayController.Post", "Calling external Rates request");
            var _ratesClientResponse = this.c_ratesClient.Lookup(
                _correlationId, 
                lookupRequest.FromCurrency,
                lookupRequest.FromCurrencyValue,
                _binClientResponse.LookupResponse.Currency).Result;

            if (_ratesClientResponse.RateStatus == RateStatus.RateServiceServerError)
            {
                this.c_reportingClient.LogActivity(_requestId, _correlationId, "APIGatewayController.Post", $"Received external Rates error response: Value:{_ratesClientResponse.RateStatus}");
                return this.StatusCode(StatusCodes.Status500InternalServerError, "bin_lookup_failure");
            }
            else
            {
                this.c_reportingClient.LogActivity(_requestId, _correlationId, "APIGatewayController.Post", $"Received external Rates success response: Value:{_ratesClientResponse.RateResponse}");
            }

            var _lookupResponse = new apigateway.Models.LookupResponse(
                lookupRequest.LookupId,
                lookupRequest.MerchantId,
                lookupRequest.FromCurrency,
                lookupRequest.FromCurrencyValue,
                _binClientResponse.LookupResponse.CardScheme,
                _binClientResponse.LookupResponse.Country,
                _binClientResponse.LookupResponse.Currency,
                _ratesClientResponse.RateResponse.ToCurrencyValue,
                _ratesClientResponse.RateResponse.ToCurrencyMarginValue,
                _ratesClientResponse.RateResponse.RateApplied,
                _ratesClientResponse.RateResponse.MarginPercentage);

            this.c_reportingClient.LogActivity(_requestId, _correlationId, "LookupController.Post", $"201 returned, response: {_lookupResponse}");

            return this.StatusCode(
                StatusCodes.Status201Created,
                _lookupResponse);
          
        }
    }
}
