using System;


namespace apigateway.External.Reporting
{
    public class LogActivity
    {
        public Guid Id { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid RequestId { get; set; }
        public string Service { get; set; }
        public string Activity { get; set; }
        public string ActivityDetail { get; set; }
        public DateTime Timestamp { get; set; }



        public override string ToString()
        {
            return $"LogActivity[Id: {this.Id}, CorrelationId:{this.CorrelationId}, RequestId:{this.RequestId}, Service:{this.Service}, Activity:{this.Activity}, ActivityDetail:{this.ActivityDetail}, Timestamp:{this.Timestamp.ToShortDateString()} ]";
        }
    }
}
