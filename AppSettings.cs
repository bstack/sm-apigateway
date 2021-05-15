using System;

namespace apigateway
{
	public class AppSettings
	{
		public string BINsRequestUri { get; private set; }
		public string RatesRequestUri { get; private set; }
		public string ReportingRequestUri { get; private set; }
	}
}