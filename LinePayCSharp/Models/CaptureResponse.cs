using Newtonsoft.Json;

namespace Line.Pay.Models
{
    /// <summary>
    /// Capture Response
    /// </summary>
    public class CaptureResponse : ResponseBase
    {
        /// <summary>
        /// Capture Information
        /// </summary>
        [JsonProperty("info")]
        public CaptureInfo Info { get; set; }
    }
}
