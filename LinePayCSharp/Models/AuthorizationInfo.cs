using Newtonsoft.Json;
using System;

namespace Line.Pay.Models
{
    public class AuthorizationInfo
    {
        /// <summary>
        /// Transaction number (19 digits)
        /// </summary>
        [JsonProperty("transactionId")]
        public Int64 TransactionId { get; set; }

        /// <summary>
        /// Transaction date (ISO 8601)
        /// </summary>
        [JsonProperty("transactionDate")]
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Transaction type
        /// </summary>
        [JsonProperty("transactionType")]
        public TransactionType TransactionType { get; set; }

        /// <summary>
        /// Payment status
        /// </summary>
        [JsonProperty("payStatus")]
        public PayStatus PayStatus { get; set; }

        /// <summary>
        /// Payment Info 
        /// </summary>
        [JsonProperty("payInfo")]
        public PayInfo[] PayInfo { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        [JsonProperty("productName")]
        public string ProductName { get; set; }

        /// <summary>
        /// Currency (ISO 4217)
        /// </summary>
        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        /// <summary>
        /// Order number of Merchant
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Authorization expiration date and time (ISO 8601)
        /// </summary>
        [JsonProperty("authorizationExpireDate")]
        public DateTime AuthorizationExpireDate { get; set; }
    }
}
