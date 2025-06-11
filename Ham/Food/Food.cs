using System;
using System.Text.Json.Serialization;
using CalorieTracker;


namespace CalorieTracker.Foods
{
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