using Factorio.NET.Converters;
using Newtonsoft.Json;

namespace Factorio.NET.Types
{
    [JsonConverter(typeof(ProductConverter))]
    public abstract class Product : FactorioType
    {
        public string Type { get; }
        
        public Product(string type)
        {
            Type = type;
        }
    }

    class FluidProduct : Product
    {
        public string Name { get; }
        
        public double Probability { get; }
        
        public double? Amount { get; }
        
        [JsonProperty("amount_min")]
        public double? AmountMin { get; }
        
        [JsonProperty("amount_max")]
        public double? AmountMax { get; }
        
        public double? Temperature { get; }
        
        public FluidProduct(string name,
            double probability = 1,
            double? amount = null,
            double? amountMin = null,
            double? amountMax = null,
            double? temperature = null) : base("fluid")
        {
            Name = name;
            Amount = amount;
            AmountMin = amountMin;
            AmountMax = amountMax;
            Probability = probability;
            Temperature = temperature;
        }
    }

    class ItemProduct : Product
    {
        public string Name { get; }
        
        public int? Amount { get; }
        
        public double Probability { get; }
        
        [JsonProperty("amount_min")]
        public int? AmountMin { get; }
        
        [JsonProperty("amount_max")]
        public int? AmountMax { get; }
        
        public ItemProduct(string name,
            int? amount = null,
            double probability = 1,
            int? amountMin = null,
            int? amountMax = null) : base("item")
        {
            Name = name;
            Probability = probability;
            AmountMin = amountMin;
            AmountMax = amountMax;
            Amount = amount;
        }
    }
}