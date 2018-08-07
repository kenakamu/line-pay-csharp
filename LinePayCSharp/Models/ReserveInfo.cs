using Newtonsoft.Json;
using System;

namespace Line.Pay.Models
{
    /// <summary>
    /// Reservation Information
    /// </summary>
    public class ReserveInfo 
    {
        /// <summary>
        /// Transaction number (19 digits)
        /// </summary>
        [JsonProperty("transactionId")]
        public Int64 TransactionId { get; set; }

        /// <summary>
        /// URL to go to after payment request
        /// </summary>
        [JsonProperty("paymentUrl")]
        public PaymentUrl PaymentUrl { get; set; }

        /// <summary>
        /// Code Value (12 digits) using for input a code instead of using a Scanner on LINE app.
        /// (Supports a Scanner of LINE Pay app above LINE 5.1 Version)
        /// </summary>
        [JsonProperty("paymentAccessToken")]
        public string PaymentAccessToken { get; set; }
    }
}
