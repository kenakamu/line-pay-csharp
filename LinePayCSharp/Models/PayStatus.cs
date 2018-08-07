using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Line.Pay.Models
{
    /// <summary>
    /// Payment status
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PayStatus
    {
        AUTHORIZATION, //  Authorized
        VOIDED_AUTHORIZATION, // Authorization voided “Void Authorization API” is called)
        EXPIRED_AUTHORIZATION // Authorization expired(The authorization period allowed by LINE Payexpired)
    }
}
