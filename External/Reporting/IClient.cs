using System;


namespace apigateway.External.Reporting
{
	public interface IClient
    {
        void LogActivity(
            string requestId,
            string correlationId,
            string activity,
            string activityDetail);
    }
}