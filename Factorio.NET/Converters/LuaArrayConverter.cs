using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Factorio.NET.Converters
{
    public class LuaArrayConverter<T> : JsonConverter<ReadOnlyCollection<T>>
    {
        public override void WriteJson(JsonWriter writer, ReadOnlyCollection<T> value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override ReadOnlyCollection<T> ReadJson(JsonReader reader, Type objectType, ReadOnlyCollection<T> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JToken jToken = JToken.Load(reader);
            var dictionary = jToken.ToObject<Dictionary<int, T>>();
            var result = new List<T>();
            
            var list = dictionary.Keys.ToList();
            list.Sort();

            foreach (int key in list)
            {
                result.Insert(key - 1, dictionary[key]);
            }

            return new ReadOnlyCollection<T>(result);
        }
    }
}