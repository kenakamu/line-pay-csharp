using Newtonsoft.Json;
using System;

namespace Line.Pay.Models
{
    /// <summary>
    /// Payment Information
    /// </summary>
    public class PaymentInfo
    {
        /// <summary>
        /// Transaction number (19 digits) received as a result after reserving a payment
        /// </summary>
        [JsonProperty("transactionId")]
        public Int64 TransactionId { get; set; }
      
        /// <summary>
        /// Transaction date & time (ISO 8601)
        /// </summary>
        [JsonProperty("transactionDate")]
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Transaction types
        /// </summary>
        [JsonProperty("transactionType")]
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Merchant's order number
        /// </summary>
        [JsonProperty("productName")]
        public string ProductName { get; set; }

        /// <summary>
        /// Merchant Name
        /// </summary>
        [JsonProperty("merchantName")]
        public string MerchantName { get; set; }

        /// <summary>
        /// Currency (ISO 4217)
        /// </summary>
        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        /// <summary>
        /// Expiration Date of Authorization (ISO 8601)
        /// </summary>
        [JsonProperty("authorizationExpireDate")]
        public DateTime AuthorizationExpireDate { get; set; }

        /// <summary>
        /// Payment Information
        /// </summary>
        [JsonProperty("payInfo")]
        public PayInfo[] PayInfo { get; set; }

        /// <summary>
        /// Refund Information
        /// </summary>
        [JsonProperty("refundList")]
        public RefundList[] RefundList { get; set; }

        /// <summary>
        /// Original payment transaction number (19 digits)
        /// </summary>
        [JsonProperty("originalTransactionId")]
        public Int64 OriginalTransactionId { get; set; }
    }
}
