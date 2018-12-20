using Factorio.NET.Converters;
using Factorio.NET.Prototypes;
using Newtonsoft.Json;

namespace Factorio.NET.Types
{
    [JsonConverter(typeof(IngredientConverter))]
    public abstract class Ingredient : FactorioType
    {
        public string Type { get; }
        
        public string Name { get; }

        private Recipe _recipe;
        public Recipe Recipe => _recipe ?? (_recipe = (Recipe) Version["recipe"][Name]);

        public Ingredient(string type, string name)
        {
            Type = type;
            Name = name;
        }
    }

    class FluidIngredient : Ingredient
    {
        public double Amount { get; }
        
        public double? Temperature { get; }
        
        [JsonProperty("minimum_temperature")]
        public double? MinimumTemperature { get; }
        
        [JsonProperty("maximum_temperature")]
        public double? MaximumTemperature { get; }
        
        public FluidIngredient(string name, double amount, double? temperature = null, double? minimumTemperature = null, double? maximumTemperature = null) : base("fluid", name)
        {
            Amount = amount;
            Temperature = temperature;
            MinimumTemperature = minimumTemperature;
            MaximumTemperature = maximumTemperature;
        }
    }

    class ItemIngredient : Ingredient
    {       
        public int Amount { get; }
        
        public ItemIngredient(string name, int amount) : base("item", name)
        {
            Amount = amount;
        }
    }
}