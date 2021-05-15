using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apigateway.External.Reporting
{
	public interface IClient
    {
        void LogActivity(
            string requestId,
            string correlationId,
            string activity,
            string activityDetail);


        Task<IEnumerable<string>> Get();
    }
}