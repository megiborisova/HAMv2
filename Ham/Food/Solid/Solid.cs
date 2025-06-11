namespace CalorieTracker.Foods
{
    public abstract class Solid : Food
    {
        protected Solid() { }

        protected Solid(string name, int calories, double carbs, double protein, double fats)
            : base(name, calories, carbs, protein, fats) { }
    }
}