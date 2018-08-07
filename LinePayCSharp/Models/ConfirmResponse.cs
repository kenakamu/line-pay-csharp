using Newtonsoft.Json;

namespace Line.Pay.Models
{
    /// <summary>
    /// Payment ConfirmAPI Response
    /// </summary>
    public class ConfirmResponse : ResponseBase
    {
        [JsonProperty("info")]
        public ConfirmInfo Info { get; set; }
    }
}
