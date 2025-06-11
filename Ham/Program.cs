using System;
using System.Collections.Generic;
using CalorieTracker.Foods;
using CalorieTracker.Users;
using CalorieTracker.Files;
using CalorieTracker.Helper;

namespace CalorieTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDir = Directory.GetCurrentDirectory();
            string foodsFile = Path.Combine(currentDir, "foods.txt"); //locates dir and file of foods

            string username = Helpers.ReadString("Enter username: ");
            User currentUser = User.LoadFromFile(username); //looks for userfiles

            List<Food> availableFoods = FoodFileManager.LoadFoodsFromFile(foodsFile); //loads foods

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n--- Calorie Tracker Menu ---");
                Console.WriteLine("1. Show available foods");
                Console.WriteLine("2. Add new food");
                Console.WriteLine("3. Log eaten food");
                Console.WriteLine("4. Show today's total calories");
                Console.WriteLine("5. Exit");

                string userChoice = Helpers.ReadString("Select option: ");
                switch (userChoice)
                {
                    case "1":
                        FoodFileManager.DisplayFoodsOrganized(availableFoods);
                        break;
                    case "2":
                        FoodFileManager.AddNewFoodFlow(foodsFile, availableFoods);
                        break;
                    case "3":
                        UserInteraction.LogEatenFood(currentUser, availableFoods);
                        currentUser.SaveToFile();   // <--- Save user data after change
                        break;

                    case "4":
                        Console.WriteLine($"Total calories eaten today: {currentUser.TotalCaloriesToday()}");
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }
    }
}