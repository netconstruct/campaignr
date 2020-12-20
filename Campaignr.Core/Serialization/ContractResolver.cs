using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Campaignr.Core.Serialization
{
    public class ContractResolver : DefaultContractResolver
    {
        private Dictionary<string, string> _propertyMappings { get; set; }

        public ContractResolver(Dictionary<string, string>  propertyMappings)
        {
            _propertyMappings = propertyMappings;
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            var resolved = _propertyMappings.TryGetValue(propertyName, out string resolvedName);
            return (resolved) ? resolvedName : base.ResolvePropertyName(propertyName);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            // skip if the property is not a DateTime
            if (property.PropertyType != typeof(DateTime))
            {
                return property;
            }

            var converter = new SafeDateConverter();
            property.Converter = converter;

            return property;
        }

    }
    class SafeDateConverter : IsoDateTimeConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null && (string.IsNullOrEmpty(reader.Value.ToString()) || reader.Value.ToString().StartsWith("0000"))) return DateTime.MinValue;
            else return base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}