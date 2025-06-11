using System;
using System.Text.Json.Serialization;
using CalorieTracker;


namespace CalorieTracker.Foods
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
    [JsonDerivedType(typeof(Alcoholic), nameof(Alcoholic))]
    [JsonDerivedType(typeof(NonAlcoholic), nameof(NonAlcoholic))]
    [JsonDerivedType(typeof(Fruit), nameof(Fruit))]
    [JsonDerivedType(typeof(Vegetable), nameof(Vegetable))]
    [JsonDerivedType(typeof(Dairy), nameof(Dairy))]
    [JsonDerivedType(typeof(Meat), nameof(Meat))]
    [JsonDerivedType(typeof(OtherAnimalProduct), nameof(OtherAnimalProduct))]
    public abstract class Food
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public double Carbs { get; set; }
        public double Protein { get; set; }
        public double Fats { get; set; }

        protected Food() { }

        protected Food(string name, int calories, double carbs, double protein, double fats)
        {
            Name = name;
            Calories = calories;
            Carbs = carbs;
            Protein = protein;
            Fats = fats;
        }

        public abstract void DisplayInfo();
    }
}