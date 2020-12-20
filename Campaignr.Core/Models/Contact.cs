using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Campaignr.Core.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public string Email { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> Data;

        public Dictionary<string, string> CustomData { get; set; }

        public Contact()
        {
            Data = new Dictionary<string, object>();
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (Data.ContainsKey("custom"))
            {
                if (Data["custom"] != null)
                {
                    var customData = Data["custom"].ToString();
                    if (!string.IsNullOrWhiteSpace(customData) && customData != "[]")
                    {
                        CustomData = JsonConvert.DeserializeObject<Dictionary<string, string>>(customData);
                    }
                }
                Data.Remove("custom");
            }
        }
    }
}