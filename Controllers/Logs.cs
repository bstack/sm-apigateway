using apigateway.External.BINs;
using apigateway.External.Rates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace apigateway.Controllers
{
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly External.BINs.IClient c_binsClient;
        private readonly External.Rates.IClient c_ratesClient; 
        private readonly External.Reporting.IClient c_reportingClient;


        public LogsController(
            [FromServices] External.BINs.IClient binsClient,
            [FromServices] External.Rates.IClient ratesClient,
            [FromServices] External.Reporting.IClient reportingClient)
        {
            this.c_binsClient = binsClient;
            this.c_ratesClient = ratesClient;
            this.c_reportingClient = reportingClient;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var _logActivityData = this.c_reportingClient.Get().Result;
            return this.StatusCode(StatusCodes.Status200OK, _logActivityData);
        }
    }
}
