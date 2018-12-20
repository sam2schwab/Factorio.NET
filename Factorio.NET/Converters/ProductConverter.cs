using System;
using Factorio.NET.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Factorio.NET.Converters
{
    public class ProductConverter : JsonConverter<Product>
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer(){ ContractResolver = new NullContractResolver()};
        
        public override void WriteJson(JsonWriter writer, Product value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override Product ReadJson(JsonReader reader, Type objectType, Product existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            string name;
            if ((name = obj["1"]?.Value<string>()) != null)
            {
                var amount = obj["2"].Value<int>();
                return new ItemProduct(name, amount);
            }

            if (obj["type"]?.Value<string>() == "fluid")
            {
                return obj.ToObject<FluidProduct>(Serializer);
            }
            return obj.ToObject<ItemProduct>(Serializer);
        }
    }
}