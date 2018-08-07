using Newtonsoft.Json;
using System.Collections.Generic;

namespace Line.Pay.Models
{
    /// <summary>
    /// Add friend list
    /// </summary>
    public class ReserveAddFriend
    {
        /// <summary>
        /// Service type (LINE_AT: line@)
        /// </summary>
        [JsonProperty("type")]
        public string Type = "LINE_AT";

        /// <summary>
        /// ID List
        /// </summary>
        [JsonProperty("idList")]
        public List<string> IdList { get; set; } = new List<string>();
    }
}
