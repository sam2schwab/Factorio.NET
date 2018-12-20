using System;
using Factorio.NET;
using Factorio.NET.Prototypes;

namespace TestApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var dataManager = new FactorioDataManager("C:/Users/Samuel/Desktop");
            var data = dataManager.GetData();
            var o = ((Recipe) data["recipe"]["rocket-silo"]).RecipeData["normal"].Ingredients[2].Recipe;
            Console.ReadKey();
        }
    }
}