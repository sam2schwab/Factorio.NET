using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Factorio.NET.Prototypes;
using Factorio.NET.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Factorio.NET.Converters
{
    public class RecipeConverter : JsonConverter<Recipe>
    {
        private static readonly JsonSerializer Serializer = new JsonSerializer(){ ContractResolver = new NullContractResolver()};
        
        public override void WriteJson(JsonWriter writer, Recipe value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override Recipe ReadJson(JsonReader reader, Type objectType, Recipe existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            JObject token = JObject.Load(reader);
            IconSpecification iconSpecs = GetIconSpecification(token); 
            var recipeData = GetRecipeData(token);
            var result = token.ToObject<Recipe>(Serializer);
            result.RecipeData = new ReadOnlyDictionary<string, RecipeData>(recipeData);
            result.IconSpecification = iconSpecs;
            return result;
        }

        private static Dictionary<string, RecipeData> GetRecipeData(JObject token)
        {
            var data = new Dictionary<string, RecipeData>();
            RecipeData expensiveRecipe;
            RecipeData normalRecipe;
            if (token["normal"] != null || token["expensive"] != null)
            {
                if (token["normal"] == null)
                {
                    expensiveRecipe = normalRecipe = token["expensive"].ToObject<RecipeData>();
                } 
                else if(token["expensive"] == null)
                {
                    expensiveRecipe = normalRecipe = token["normal"].ToObject<RecipeData>();
                }
                else //if (token["normal"] != null && token["expensive"] != null)
                {
                    if (token["normal"] is JValue && !token["normal"].ToObject<bool>())
                    {
                        expensiveRecipe = token["expensive"].ToObject<RecipeData>();
                        normalRecipe = token["expensive"].ToObject<RecipeData>();
                        normalRecipe.Enabled = false;
                    } 
                    else if(token["normal"] is JValue && !token["expensive"].ToObject<bool>())
                    {
                        normalRecipe = token["normal"].ToObject<RecipeData>();
                        expensiveRecipe = token["normal"].ToObject<RecipeData>();
                        expensiveRecipe.Enabled = false;
                    }
                    else
                    {
                        expensiveRecipe = token["expensive"].ToObject<RecipeData>();
                        normalRecipe = token["normal"].ToObject<RecipeData>();
                    }
                }
            }
            else
            {
                normalRecipe = token.ToObject<RecipeData>();
                expensiveRecipe = normalRecipe;
            }

            data.Add("normal", normalRecipe);
            data.Add("expensive", expensiveRecipe);
            return data;
        }

        private static IconSpecification GetIconSpecification(JObject token)
        {
            List<IconData> iconsData = null;
            if (token["icons"] != null)
            {
                iconsData = token["icons"].Value<List<IconData>>();
            }
            else if (token["icon"] != null)
            {
                iconsData = new List<IconData>() {new IconData(token["icon"].Value<string>())};
            }

            if (token["icon_size"] != null)
            {
                iconsData?.ForEach(icon =>
                {
                    if (!icon.IconSize.HasValue)
                    {
                        icon.IconSize = token["icon_size"].Value<int>();
                    }
                });
            }

            return iconsData == null ? null : new IconSpecification(iconsData);
        }
    }
}