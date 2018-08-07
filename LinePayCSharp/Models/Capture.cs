using Newtonsoft.Json;

namespace Line.Pay.Models
{
    /// <summary>
    /// Capture
    /// </summary>
    public class Capture
    {
        /// <summary>
        /// Payment amount
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// Payment currency (ISO 4217)
        /// </summary>
        [JsonProperty("currency")]
        public Currency Currency { get; set; }
    }
}
