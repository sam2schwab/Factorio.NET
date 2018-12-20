using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Factorio.NET.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Factorio.NET.Converters
{
    public class RecipeDataConverter : JsonConverter<RecipeData>
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer()
            {ContractResolver = new NullContractResolver()};

        public override void WriteJson(JsonWriter writer, RecipeData value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override RecipeData ReadJson(JsonReader reader, Type objectType, RecipeData existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            JObject token = JObject.Load(reader);
            ReadOnlyCollection<Product> results = GetResults(token);
            var result = token.ToObject<RecipeData>(Serializer);
            result.Results = results;
            return result;
        }

        private ReadOnlyCollection<Product> GetResults(JObject token)
        {
            if (token["result"] != null)
            {
                var name = token["result"].Value<string>();
                var amount = 1;
                if (token["result_count"] != null)
                {
                    amount = token["result_count"].Value<int>();
                }

                List<Product> products = new List<Product>() {new ItemProduct(name, amount)};
                
                return new ReadOnlyCollection<Product>(products);
            }
            else //if (token["results"] != null)
            {
                return token["results"].ToObject<ReadOnlyCollection<Product>>(new JsonSerializer()
                    {Converters = {new LuaArrayConverter<Product>()}});
            }
        }
    }
}