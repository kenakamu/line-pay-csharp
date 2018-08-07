using Newtonsoft.Json;

namespace Line.Pay.Models
{
    /// <summary>
    /// Preapproved Payment Response
    /// </summary>
    public class PreApprovedPayResponse : ResponseBase
    {
        [JsonProperty("info")]
        public PreApprovedPayInfo Info { get; set; }       
    }
}
