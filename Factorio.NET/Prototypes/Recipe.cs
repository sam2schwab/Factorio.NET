using System.Collections.Generic;
using System.Collections.ObjectModel;
using Factorio.NET.Converters;
using Factorio.NET.Types;
using Newtonsoft.Json;

namespace Factorio.NET.Prototypes
{
    [JsonConverter(typeof(RecipeConverter))]
    public class Recipe : Prototype
    {
        public string Category { get; }
        
        public string Subgroup { get; }

        public IconSpecification IconSpecification { get; internal set; }

        [JsonProperty("crafting_machine_tint")]
        public ReadOnlyDictionary<string, Color> CraftingMachineTint { get; }

        public ReadOnlyDictionary<string, RecipeData> RecipeData { get; internal set; }

        public Recipe(string type,
            string name,
            string category,
            string subgroup,
            ReadOnlyDictionary<string, Color> craftingMachineTint,
            ReadOnlyCollection<string> localisedName,
            ReadOnlyCollection<string> localisedDescription,
            string order) : base(type,
            name,
            localisedName,
            localisedDescription,
            order)
        {
            Category = category;
            Subgroup = subgroup;
            CraftingMachineTint = craftingMachineTint;
            if (craftingMachineTint == null) return;
            foreach (var keyValuePair in craftingMachineTint)
            {
                keyValuePair.Value.Version = Version;
            }
        }

        protected override void OnVersionSet()
        {
            foreach (var keyValuePair in RecipeData)
            {
                RecipeData recipeData = keyValuePair.Value;
                recipeData.Version = Version;
                foreach (Ingredient ingredient in recipeData.Ingredients)
                {
                    ingredient.Version = Version;
                }
                foreach (Product product in recipeData.Results)
                {
                    product.Version = Version;
                }
            }

            if (IconSpecification != null )
                IconSpecification.Version = Version;
        }
    }
}