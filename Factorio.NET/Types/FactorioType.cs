using Newtonsoft.Json;

namespace Factorio.NET.Types
{
    public class FactorioType
    {
        [JsonIgnore]
        public FactorioData Version { get; internal set; }
    }
}