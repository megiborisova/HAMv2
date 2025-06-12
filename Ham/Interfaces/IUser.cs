using CalorieTracker.Foods;

namespace CalorieTracker.Iface
{
    public interface IUser
    {
        public string Username { get; set; }

        public void AddFood(Food food);
        public void SaveToFile();
        public int TotalCaloriesToday();
        public void DisplayStats();
    }
}