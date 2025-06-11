using CalorieTracker.Foods;
using System;

namespace CalorieTracker.Factory
{
    public static class FoodFactory
    {
        public static Food? CreateFood(string category, string name, int calories, double carbs, double protein, double fats,
                                       double? volume = null, double? alcoholPercent = null)
        {
            switch (category.ToLower())
            {
                case "fruit":
                    return new Fruit(name, calories, carbs, protein, fats);
                case "vegetable":
                    return new Vegetable(name, calories, carbs, protein, fats);
                case "alcoholic":
                    if (volume.HasValue && alcoholPercent.HasValue)
                        return new Alcoholic(name, calories, volume.Value, alcoholPercent.Value, carbs, protein, fats);
                    break;
                case "nonalcoholic":
                    if (volume.HasValue)
                        return new NonAlcoholic(name, calories, volume.Value, carbs, protein, fats);
                    break;
                case "meat":
                    return new Meat(name, calories, carbs, protein, fats);
                case "dairy":
                    return new Dairy(name, calories, carbs, protein, fats);
                case "otheranimalbased":
                    return new OtherAnimalProduct(name, calories, carbs, protein, fats);
                default:
                    Console.WriteLine("Unknown category.");
                    break;
            }
            return null;
        }
        
    }
}
