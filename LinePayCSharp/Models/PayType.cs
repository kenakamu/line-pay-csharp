using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Line.Pay.Models
{
    /// <summary>
    /// Payment types
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PayType
    {
        NORMAL, // Single payment (Default Value)
        PREAPPROVED // Preapproved payment
    }
}
