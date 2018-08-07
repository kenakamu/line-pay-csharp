using Newtonsoft.Json;

namespace Line.Pay.Models
{
    /// <summary>
    /// URL to go to after payment request
    /// </summary>
    public class PaymentUrl
    {
        /// <summary>
        /// Web URL to go to after payment reques
        /// Used if payment request was made in Web environment
        /// URL to the LINE Pay payment waiting screen
        /// Redirected to the provided URL without any additional parameters
        /// When a pop-up browser from a Desktop is opened, Size - Width: 700px, Height : 546px
        /// </summary>
        [JsonProperty("web")]
        public string Web { get; set; }

        /// <summary>
        /// App URL to the Payment Screen        /// Used if payment request was made in an app        /// Redirecting URL from Merchant's app to LINE Pay App
        /// </summary>
        [JsonProperty("app")]
        public string App { get; set; }
    }
}
