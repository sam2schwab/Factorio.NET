using System;
using Factorio.NET.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Factorio.NET.Converters
{
    public class IngredientConverter : JsonConverter<Ingredient>
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer(){ ContractResolver = new NullContractResolver()};
        
        public override void WriteJson(JsonWriter writer, Ingredient value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override Ingredient ReadJson(JsonReader reader, Type objectType, Ingredient existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            string name;
            if ((name = obj["1"]?.Value<string>()) != null)
            {
                var amount = obj["2"].Value<int>();
                return new ItemIngredient(name, amount);
            }

            if (obj["type"].Value<string>() == "fluid")
            {
                return obj.ToObject<FluidIngredient>(Serializer);
            }
            return obj.ToObject<ItemIngredient>(Serializer);
        }
    }
}