using System;

namespace CalorieTracker.Foods
{
    public class Meat : AnimalBased
    {
        public Meat() { }

        public Meat(string name, int calories, double carbs, double protein, double fats)
            : base(name, calories, carbs, protein, fats) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"{Name} - {Calories} kcal (Meat) - {Carbs}g carbs, {Protein}g protein, {Fats}g fats");
        }
    }
}