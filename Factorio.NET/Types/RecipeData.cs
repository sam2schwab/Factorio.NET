using System.Collections.Generic;
using System.Collections.ObjectModel;
using Factorio.NET.Converters;
using Newtonsoft.Json;

namespace Factorio.NET.Types
{
    [JsonConverter(typeof(RecipeDataConverter))]
    public class RecipeData : FactorioType
    {
        [JsonConverter(typeof(LuaArrayConverter<Ingredient>))]
        public ReadOnlyCollection<Ingredient> Ingredients { get; }

        [JsonIgnore]
        public ReadOnlyCollection<Product> Results { get; internal set; }

        [JsonProperty("energy_required")]
        public double EnergyRequired { get; }

        [JsonProperty("emissions_multiplier")]
        public double EmissionsMultiplier { get; }
        
        [JsonProperty("requester_paste_multiplier")]
        public int RequesterPasteMultiplier { get; }
        
        [JsonProperty("overload_multiplier")]
        public int OverloadMultiplier { get; }
        
        public bool Enabled { get; internal set; }
        
        public bool Hidden { get; }
        
        [JsonProperty("hide_from_stats")]
        public bool HideFromStats { get; }
        
        [JsonProperty("allow_decomposition")]
        public bool AllowDecomposition { get; }
        
        [JsonProperty("allow_as_intermediate")]
        public bool AllowAsIntermediate { get; }
        
        [JsonProperty("allow_intermediates")]
        public bool AllowIntermediates { get; }
        
        [JsonProperty("always_show_made_in")]
        public bool AlwaysShowMadeIn { get; }
        
        [JsonProperty("show_amount_in_title")]
        public bool ShowAmountInTitle { get; }
        
        [JsonProperty("always_show_products")]
        public bool AlwaysShowProducts { get; }
        
        [JsonProperty("main_product")]
        public string MainProduct { get; }

        public RecipeData(ReadOnlyCollection<Ingredient> ingredients,
            bool hideFromStats,
            bool allowDecomposition,
            bool allowAsIntermediate,
            bool allowIntermediates,
            bool alwaysShowMadeIn,
            bool showAmountInTitle,
            bool alwaysShowProducts,
            string mainProduct,
            double energyRequired = 0.5,
            double emissionsMultiplier = 1.0,
            int requesterPasteMultiplier = 30,
            int overloadMultiplier = 0,
            bool enabled = true,
            bool hidden = false)
        {
            Ingredients = ingredients;
            HideFromStats = hideFromStats;
            AllowDecomposition = allowDecomposition;
            AllowAsIntermediate = allowAsIntermediate;
            AllowIntermediates = allowIntermediates;
            AlwaysShowMadeIn = alwaysShowMadeIn;
            ShowAmountInTitle = showAmountInTitle;
            AlwaysShowProducts = alwaysShowProducts;
            MainProduct = mainProduct;
            Hidden = hidden;
            Enabled = enabled;
            OverloadMultiplier = overloadMultiplier;
            RequesterPasteMultiplier = requesterPasteMultiplier;
            EnergyRequired = energyRequired;
            EmissionsMultiplier = emissionsMultiplier;
        }
    }
}