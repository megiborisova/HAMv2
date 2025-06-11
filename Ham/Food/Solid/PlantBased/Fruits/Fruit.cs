using System;

namespace CalorieTracker.Foods
{
    public class Fruit : PlantBased
    {
        public Fruit() { }

        public Fruit(string name, int calories, double carbs, double protein, double fats)
            : base(name, calories, carbs, protein, fats) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"{Name} - {Calories} kcal (Fruit) - {Carbs}g carbs, {Protein}g protein, {Fats}g fats");
        }
    }
}