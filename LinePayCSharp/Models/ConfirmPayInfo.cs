using Newtonsoft.Json;

namespace Line.Pay.Models
{

    /// <summary>
    /// Confirm Payment Information
    /// </summary>
    public class ConfirmPayInfo
    {
        /// <summary>
        /// Payment method used 
        /// </summary>
        [JsonProperty("method")]
        public PayMethod Method { get; set; }

        /// <summary>
        /// Payment amount
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// For opt-Preapproved Payment, Credit card nickname
        /// Initially registered credit card name managed by LINE Pay.
        /// If LINE Pay user has not set a nickname, blank text is displayed.
        /// The nickname can be changed when requested by the user in LINE Pay and changed name is not share with the Merchant
        /// </summary>
        [JsonProperty("creditCardNickname")]
        public string CreditCardNickname { get; set; }

        /// <summary>
        /// For opt-Preapproved Payment, Credit card brand
        /// </summary>
        [JsonProperty("creditCardBrand")]
        public CreditCardBrand CreditCardBrand { get; set; }        
    }
}
