namespace CalorieTracker.Foods
{
    public abstract class AnimalBased : Solid
    {
        protected AnimalBased() { }

        protected AnimalBased(string name, int calories, double carbs, double protein, double fats)
            : base(name, calories, carbs, protein, fats) { }
    }
}