namespace CalorieTracker.Iface
{
    public interface IFood
    {
        string Name { get; set; }
        int Calories { get; set; }
        double Protein { get; set; }
        double Fats { get; set; }
        double Carbs { get; set; }
        public abstract void DisplayInfo();
    }
}