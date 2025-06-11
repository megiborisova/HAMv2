using System;

namespace CalorieTracker.Foods
{
    public class Vegetable : PlantBased
    {
        public Vegetable() { }

        public Vegetable(string name, int calories, double carbs, double protein, double fats)
            : base(name, calories, carbs, protein, fats) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"{Name} - {Calories} kcal (Vegetable) - {Carbs}g carbs, {Protein}g protein, {Fats}g fats");
        }
    }
}
