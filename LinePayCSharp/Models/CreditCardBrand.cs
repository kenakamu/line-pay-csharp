using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Line.Pay.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CreditCardBrand
    {
        VISA,
        MASTER,
        AMEX,
        DINERS,
        JCB
    }
}
