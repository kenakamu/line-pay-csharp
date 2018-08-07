using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Line.Pay.Models
{
    /// <summary>
    /// Type of URL that the buyer is redirected to after selecting a payment method and entering the payment password in LINE Pay
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ConfirmUrlType
    {
        CLIENT, // A user based URL (default)
        SERVER  // A server based URL. Users just need to check the payment information screen in LINE Pay which then notifies the Merchant server that the payment is available.    }
}
