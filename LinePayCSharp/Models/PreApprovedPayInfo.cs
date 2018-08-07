using Newtonsoft.Json;
using System;

namespace Line.Pay.Models
{
    public class PreApprovedPayInfo
    {
        /// <summary>
        /// Transaction number (19 digits)
        /// </summary>
        [JsonProperty("transactionId")]
        public Int64 TransactionId { get; set; }

        /// <summary>
        /// Transaction date and time (ISO 8601)
        /// </summary>
        [JsonProperty("transactionDate")]
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Expiration Date of Authorization (ISO 8601)
        /// Deliver for a transaction authorized only. (Capture= false)
        /// </summary>
        [JsonProperty("authorizationExpireDate")]
        public DateTime AuthorizationExpireDate { get; set; }
    }
}
