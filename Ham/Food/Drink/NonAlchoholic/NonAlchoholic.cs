using System;

namespace CalorieTracker.Foods
{
    public class NonAlcoholic : Drinks
    {
        public NonAlcoholic() { }

        public NonAlcoholic(string name, int calories, double volume, double carbs, double protein, double fats)
            : base(name, calories, volume, carbs, protein, fats) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"{Name} - {Calories} kcal - {VolumeMl}ml (Non-alcoholic) - {Carbs}g carbs, {Protein}g protein, {Fats}g fats");
        }
    }
}