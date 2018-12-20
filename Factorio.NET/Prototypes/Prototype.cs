using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Factorio.NET.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLua;

namespace Factorio.NET.Prototypes
{
    public class Prototype
    {
        private FactorioData _version;
        [JsonIgnore]
        public FactorioData Version
        {
            get => _version;
            internal set
            {
                _version = value;
                OnVersionSet();
            }
        }

        protected virtual void  OnVersionSet() {}
        
        public string Type { get; }

        public string Name { get; }

        public string Order { get; }

        [JsonProperty("localised_name")]
        [JsonConverter(typeof(LuaArrayConverter<string>))]
        public ReadOnlyCollection<string> LocalisedName { get; }

        [JsonProperty("localised_description")]
        [JsonConverter(typeof(LuaArrayConverter<string>))]
        public ReadOnlyCollection<string> LocalisedDescription { get; }

        public Prototype(string type, string name, ReadOnlyCollection<string> localisedName = null, ReadOnlyCollection<string> localisedDescription = null, string order = null)
        {
            Type = type;
            Name = name;
            LocalisedName = localisedName;
            LocalisedDescription = localisedDescription;
            Order = order;
        }

        public static Prototype Parse(LuaTable value)
        {
            JObject json = LuaToJson(value);
            Type type;
            switch (json["type"].ToString())
            {
                case "recipe":
                    type = typeof(Recipe);
                    break;
                default:
                    type = typeof(Prototype);
                    break;
            }
            
            return (Prototype) json.ToObject(type);
        }

        private static JObject LuaToJson(LuaTable table)
        {
            var jObject = new JObject();
            foreach (object key in table.Keys)
            {
                object value = table[key] is LuaTable subTable ? LuaToJson(subTable) : table[key];
                jObject.Add(key.ToString(), JToken.FromObject(value));
            }
            return jObject;
        }
    }
}