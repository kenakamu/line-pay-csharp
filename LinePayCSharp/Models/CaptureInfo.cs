using Newtonsoft.Json;
using System;

namespace Line.Pay.Models
{
    /// <summary>
    /// Capture Response
    /// </summary>
    public class CaptureInfo
    {
        /// <summary>
        /// Order number sent from Merchant upon reserving a payment
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Transaction number (19 digits) received as a result after reserving a payment
        /// </summary>
        [JsonProperty("transactionId")]
        public Int64 TransactionId { get; set; }

        /// <summary>
        /// Confirm Payment Information
        /// </summary>
        [JsonProperty("payInfo")]
        public PayInfo[] PayInfo { get; set; }
    }
}
