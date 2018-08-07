using Newtonsoft.Json;

namespace Line.Pay.Models
{
    /// <summary>
    /// Refund
    /// </summary>
    public class Refund
    {
        [JsonProperty("refundAmount")]
        public int RefundAmount { get; set; }
    }
}
