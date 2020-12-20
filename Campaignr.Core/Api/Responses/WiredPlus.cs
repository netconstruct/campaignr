using Newtonsoft.Json;

namespace Campaignr.Core.Api.Responses
{
    public class AccountInfo
    {
        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("number_of_contacts")]
        public int Contacts { get; set; }
    }
}
