using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kontest.WebApi.CustomJsonConverters
{
    public class EnumToKeyValuePairConverter : StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string name = Enum.GetName(value.GetType(), value);

            writer.WriteStartObject();
            writer.WritePropertyName("name");
            writer.WriteValue(name);
            writer.WritePropertyName("value");
            writer.WriteValue(value);
            writer.WriteEndObject();
        }
    }
}
