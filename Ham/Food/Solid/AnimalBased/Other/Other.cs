using System;

namespace CalorieTracker.Foods
{
    public class OtherAnimalProduct : AnimalBased
    {
        public OtherAnimalProduct() { }

        public OtherAnimalProduct(string name, int calories, double carbs, double protein, double fats)
            : base(name, calories, carbs, protein, fats) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"{Name} - {Calories} kcal (Other Animal-Based) - {Carbs}g carbs, {Protein}g protein, {Fats}g fats");
        }
    }
}