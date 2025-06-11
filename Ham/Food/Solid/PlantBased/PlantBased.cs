namespace CalorieTracker.Foods
{
    public abstract class PlantBased : Solid
    {
        protected PlantBased() { }

        protected PlantBased(string name, int calories, double carbs, double protein, double fats)
            : base(name, calories, carbs, protein, fats) { }
    }
}