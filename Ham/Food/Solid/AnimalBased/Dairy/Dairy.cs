using System;

namespace CalorieTracker.Foods
{
    public class Dairy : AnimalBased
    {
        public Dairy() { }

        public Dairy(string name, int calories, double carbs, double protein, double fats)
            : base(name, calories, carbs, protein, fats) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"{Name} - {Calories} kcal (Dairy) - {Carbs}g carbs, {Protein}g protein, {Fats}g fats");
        }
    }
}