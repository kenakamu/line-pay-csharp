using Newtonsoft.Json;
using System;

namespace Line.Pay.Models
{
    /// <summary>
    /// Payment Confirmation Information
    /// </summary>
    public class ConfirmInfo
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
        /// opt-authorization expiration date (ISO 8601)
        /// </summary>
        [JsonProperty("authorizationExpireDate")]
        public DateTime AuthorizationExpireDate { get; set; }

        /// <summary>
        /// Confirm Payment Information
        /// </summary>
        [JsonProperty("payInfo")]
        public ConfirmPayInfo[] PayInfo { get; set; }

        /// <summary>
        /// opt-Key for preapproved payment (15 digits)
        /// </summary>
        [JsonProperty("regKey")]
        public string RegKey { get; set; }
    }
}
