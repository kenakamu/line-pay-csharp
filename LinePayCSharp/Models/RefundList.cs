using Newtonsoft.Json;
using System;

namespace Line.Pay.Models
{
    /// <summary>
    /// Refund Information
    /// </summary>
    public class RefundList
    {
        /// <summary>
        /// Refunded transaction number (19 digits)
        /// </summary>
        [JsonProperty("refundTransactionId")]
        public Int64 RefundTransactionId { get; set; }

        /// <summary>
        /// Transaction types
        /// </summary>
        [JsonProperty("transactionType")]
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Refund Amount
        /// </summary>
        [JsonProperty("refundAmount")]
        public int RefundAmount { get; set; }

        /// <summary>
        /// Refunded transaction date & time (ISO 8601)
        /// </summary>
        [JsonProperty("refundTransactionDate")]
        public DateTime RefundTransactionDate { get; set; }
    }
}
