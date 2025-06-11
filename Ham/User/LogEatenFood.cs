using System;
using CalorieTracker;
using CalorieTracker.Foods;
using CalorieTracker.Users;
using System.Collections.Generic;

namespace CalorieTracker.Users
{
    static class UserInteraction
    {
        public static void LogEatenFood(User user, List<Food> availableFoods)
        {
        // Group foods by category/type
            var grouped = new Dictionary<string, List<Food>>();

            foreach (var food in availableFoods)
            {
                string type = food.GetType().Name;
                if (!grouped.ContainsKey(type))
                    grouped[type] = new List<Food>();
                grouped[type].Add(food);
            }

            // Show food categories
            Console.WriteLine("\n### Select a food category to log:");
            var categoryList = new List<string>(grouped.Keys);
            for (int i = 0; i < categoryList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {categoryList[i]}");
            }
            //choice for type of food (dairy,meat ...)
            Console.Write("Enter category number: ");
            if (!int.TryParse(Console.ReadLine(), out int categoryChoice) || categoryChoice < 1 || categoryChoice > categoryList.Count)
            {
                Console.WriteLine("Invalid category selection.");
                return;
            }

            string selectedCategory = categoryList[categoryChoice - 1];
            var foodsInCategory = grouped[selectedCategory];

            // Show foods within selected category
            Console.WriteLine($"\n--- {selectedCategory} options ---");
            for (int i = 0; i < foodsInCategory.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                foodsInCategory[i].DisplayInfo();
            }
            // Shows foods and lets user select food
            Console.Write("Select food number to log: ");
            if (int.TryParse(Console.ReadLine(), out int foodChoice) &&
            foodChoice >= 1 && foodChoice <= foodsInCategory.Count)
            {
                var selectedFood = foodsInCategory[foodChoice - 1];
                user.AddFood(selectedFood);
                Console.WriteLine($"Logged {selectedFood.Name}.");
            }
            else
                Console.WriteLine("Invalid food selection.");
        }
    }
}