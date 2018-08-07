using Newtonsoft.Json;
using System.Collections.Generic;

namespace Line.Pay.Models
{
    /// <summary>
    /// Extra information for reserve
    /// </summary>
    public class ReserveExtras
    {
        /// <summary>
        /// Add friend list
        /// </summary>
        [JsonProperty("addFriends")]
        public List<ReserveAddFriend> ReserveAddFriends { get; set; }

        /// <summary>
        /// Branch store name where request the reserve.
        /// </summary>
        [JsonProperty("branchName")]
        public string BranchName { get; set; }
    }
}
