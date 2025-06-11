namespace CalorieTracker.Foods
{
    public abstract class Drinks : Food
    {
        public double VolumeMl { get; set; }

        protected Drinks() { }

        protected Drinks(string name, int calories, double volume, double carbs, double protein, double fats)
            : base(name, calories, carbs, protein, fats)
        {
            VolumeMl = volume;
        }
    }
}