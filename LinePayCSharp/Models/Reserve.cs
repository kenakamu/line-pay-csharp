using Newtonsoft.Json;

namespace Line.Pay.Models
{
    /// <summary>
    /// Reserve the payment
    /// </summary>
    public class Reserve
    {
        /// <summary>
        /// Product Name (charset: UTF-8)
        /// </summary>
        [JsonProperty("productName")]
        public string ProductName { get; set; }

        /// <summary>
        /// Product image URL
        /// Image URL to be displayed on the Payment screen
        /// Size: 84 x 84 (Image to be displayed only on the Payment screen.Recommended to follow the guidelines)        /// </summary>
        [JsonProperty("productImageUrl")]
        public string ProductImageUrl { get; set; }

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
        /// LINE member ID: LINE user mid for making a payment
        /// </summary>
        [JsonProperty("mid")]
        public string Mid { get; set; }
        [JsonProperty("oneTimeKey")]

        ///Result of scanning and reading QR/Bar code information given by LINE Pay app is used as a LINE Pay user’s
        ///mid.Valid time is 5 minutes and it will be deleted with reserve at the same time.
        ///Supports QR/BarCode above LINE 5.1 Version of LINE Pay app
        public string OneTimeKey { get; set; }

        /// <summary>
        /// Merchant's URL that the buyer is redirected to after selecting a payment method and entering the payment password in LINE Pay.
        /// On the redirected URL, Merchant can call Confirm Payment API and complete the payment
        /// LINE Pay passes an additional parameter, "transactionId.".
        /// </summary>
        [JsonProperty("confirmUrl")]
        public string ConfirmUrl { get; set; }

        /// <summary>
        /// Type of URL that the buyer is redirected to after selecting a payment method and entering the payment password in LINE Pay
        /// </summary>
        [JsonProperty("confirmUrlType")]
        public ConfirmUrlType ConfirmUrlType { get; set; }

        /// <summary>
        /// When moved to confirmUrl, Check a browser
        /// true: When a browser calling a payment and a browser directing to confirmUrl are not identical, LINE Pay provides a Guide Page directing to a previous browse.
        /// false(Default Value): Directing to ConfirmUrl without checking a browser
        /// </summary>
        [JsonProperty("checkConfirmUrlBrowser")]
        public bool? CheckConfirmUrlBrowser { get; set; }

        /// <summary>
        /// Payment Cancellation page URL 
        /// The URL redirected to, from the LINE App Payment page when the LINE Pay user cancels payment(URL for the Merchant accessing via mobile devices to go to the Merchant's app or website when the payment is canceled)
        /// URL sent by Merchant is used as is
        /// No additional parameters sent by LINE Pay
        /// </summary>
        [JsonProperty("cancelUrl")]
        public string CancelUrl { get; set; }

        /// <summary>
        /// Information to avoid phishing during transition between apps in Android.
        /// </summary>
        [JsonProperty("packageName")]
        public string PackageName { get; set; }

        /// <summary>
        /// Merchant's order number corresponding to the payment reques
        /// A unique number managed by a Merchant
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Recipient contact (for Risk Management)
        /// </summary>
        [JsonProperty("deliveryPlacePhone")]
        public string DeliveryPlacePhone { get; set; }

        /// <summary>
        /// Payment types
        /// </summary>
        [JsonProperty("payType")]
        public PayType PayType { get; set; }

        /// <summary>
        /// Language codes on the payment waiting screen
        /// Language codes are not mandatory but if not received, multiple languages are supported based on the accept-language header.
        /// If an unsupported langCd is received, English ("en") is used by default. 
        /// BCP-47 format: http://en.wikipedia.org/wiki/IETF_language_tag
        /// </summary>
        [JsonProperty("langCd")]
        public LanguageCode LanguageCode { get; set; }

        /// <summary>
        /// Whether to capture or not
        /// true: Payment authorization and capture are handled at once when the Confirm Payment API is called(default)
        /// false: A payment is completed only after it is authorized and then separately captured by calling "Capture API" , when the Confirm Payment API is called
        /// </summary>
        [JsonProperty("capture")]
        public bool? Capture { get; set; }

        /// <summary>
        /// Extra information for reserve
        /// </summary>
        [JsonProperty("extras")]
        public ReserveExtras ReserveExtras { get; set; }
    }
}
