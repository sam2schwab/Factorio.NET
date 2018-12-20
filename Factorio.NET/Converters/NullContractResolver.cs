using System;
using Factorio.NET.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Factorio.NET.Converters
{
    public class NullContractResolver : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            return null;
        }
    }
}