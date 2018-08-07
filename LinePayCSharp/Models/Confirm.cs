using Newtonsoft.Json;

namespace Line.Pay.Models
{
    /// <summary>
    /// Confirm class
    /// </summary>
    public class Confirm
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
