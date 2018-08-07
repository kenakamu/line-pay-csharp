using Newtonsoft.Json;
using System;

namespace Line.Pay.Models
{
    /// <summary>
    /// Refund Information
    /// </summary>
    public class RefundInfo
    {
        /// <summary>
        /// Refunded transaction number (newly issued number - 19 digits)
        /// </summary>
        [JsonProperty("refundTransactionId")]
        public Int64 RefundTransactionId { get; set; }

        /// <summary>
        /// Refunded transaction date & time (ISO 8601)
        /// </summary>
        [JsonProperty("refundTransactionDate")]
        public DateTime RefundTransactionDate { get; set; }
    }
}
