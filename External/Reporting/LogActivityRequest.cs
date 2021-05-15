using System;


namespace apigateway.External.Reporting
{
    public class LogActivityRequest
    {
        public string Service { get; }
        public string Activity { get; }
        public string ActivityDetail { get; }


        public LogActivityRequest(
            string activity,
            string activityDetail)
        {
            this.Service = "apigateway";
            this.Activity = activity;
            this.ActivityDetail = activityDetail;
        } 
    }
}
