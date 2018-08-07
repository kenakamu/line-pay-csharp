using Newtonsoft.Json;

namespace Line.Pay.Models
{
    /// <summary>
    /// Refund Payment Response
    /// </summary>
    public class RefundResponse : ResponseBase
    {
        /// <summary>
        /// Refund Information
        /// </summary>
        [JsonProperty("info")]
        public RefundInfo Info { get; set; }
    }
}
