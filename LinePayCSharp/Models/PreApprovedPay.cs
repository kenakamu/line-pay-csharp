using Newtonsoft.Json;

namespace Line.Pay.Models
{
    /// <summary>
    /// Pre-approved Payment
    /// </summary>
    public class PreApprovedPay
    {
        /// <summary>
        /// Product name
        /// </summary>
        [JsonProperty("productName")]
        public string ProductName { get; set; }

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

        /// <summary>
        /// Merchant's order number corresponding to the payment request.
        /// A unique number managed by a Merchant
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Whether to be captured or not.
        /// true(Default Value): Authorization and capture are handled at once
        /// false: Handled Capture process and Called "Capture API" in order to complete a payment.
        /// </summary>
        [JsonProperty("capture")]
        public bool? Capture { get; set; }
    }
}
