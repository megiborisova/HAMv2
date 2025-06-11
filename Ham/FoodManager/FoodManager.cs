using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalorieTracker.Helper;
using CalorieTracker.Foods;
using CalorieTracker.Factory;
using System.Transactions;




namespace CalorieTracker.Files
{
    public static class FoodFileManager //Works with Food file (adding, reading, displaying)
    {
        public static List<Food> LoadFoodsFromFile(string filepath) //Reading Food.txt line by line
        {
            var foods = new List<Food>();
            if (!File.Exists(filepath)) //file existance check
            {
                Console.WriteLine($"Food file {filepath} not found. Starting empty.");
                return foods;
            }

            string[] lines = File.ReadAllLines(filepath); //reads from path in array
            string currentCategory = "";

            foreach (var line in lines) //cycling through the txt file
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("#")) //seperates the "Types of food"
                {
                    currentCategory = line.Substring(1).Trim();
                    continue;
                }

                string[] parts = line.Split(';'); // splits where it finds ";"
                try //since data is stored as protein;carbs;fats;
                {
                    switch (currentCategory.ToLower()) 
                    {
                        case "fruit":
                            foods.Add(new Fruit(parts[0], int.Parse(parts[1]), double.Parse(parts[2]), double.Parse(parts[3]), double.Parse(parts[4])));
                            break;
                        case "vegetable":
                            foods.Add(new Vegetable(parts[0], int.Parse(parts[1]), double.Parse(parts[2]), double.Parse(parts[3]), double.Parse(parts[4])));
                            break;
                        case "alcoholic":
                            foods.Add(new Alcoholic(parts[0], int.Parse(parts[1]), double.Parse(parts[2]), double.Parse(parts[3]),
                                                    double.Parse(parts[4]), double.Parse(parts[5]), double.Parse(parts[6])));
                            break;
                        case "nonalcoholic":
                            foods.Add(new NonAlcoholic(parts[0], int.Parse(parts[1]), double.Parse(parts[2]),
                                                       double.Parse(parts[3]), double.Parse(parts[4]), double.Parse(parts[5])));
                            break;
                        case "meat":
                            foods.Add(new Meat(parts[0], int.Parse(parts[1]), double.Parse(parts[2]), double.Parse(parts[3]), double.Parse(parts[4])));
                            break;
                        case "dairy":
                            foods.Add(new Dairy(parts[0], int.Parse(parts[1]), double.Parse(parts[2]), double.Parse(parts[3]), double.Parse(parts[4])));
                            break;
                        case "otheranimalbased":
                            foods.Add(new OtherAnimalProduct(parts[0], int.Parse(parts[1]), double.Parse(parts[2]), double.Parse(parts[3]), double.Parse(parts[4])));
                            break;
                        default:
                            Console.WriteLine($"Unknown category {currentCategory}, skipping {line}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line: {line} - {ex.Message}");
                }
            }
            return foods;
        }

        public static void AddNewFoodFlow(string foodsFile, List<Food> availableFoods) //Adding new food
        {
            string? category = Helpers.ReadString("Enter category (Fruit, Vegetable, Alcoholic, NonAlcoholic, Meat, Dairy, OtherAnimalBased):");
            category.Trim();
            string? name = Helpers.ReadString("Enter Food name: ");
            int calories = Helpers.ReadInt("Enter Calories: ");
            double carbs = Helpers.ReadDouble("Enter carbs(g): ");
            double protein = Helpers.ReadDouble("Enter protein (g): ");
            double fats = Helpers.ReadDouble("Enter fats (g): ");
            double? volume = null;
            double? alcoholPercent = null;

        if (category?.Equals("alcoholic", StringComparison.OrdinalIgnoreCase) == true)
        {
            double volA = Helpers.ReadDouble("Enter volume (ml): ");
            volume = volA;
            double alcPercent = Helpers.ReadDouble("Enter alcohol percentage: ");
            alcoholPercent = alcPercent;
        }
        
        else if (category?.Equals("nonalcoholic", StringComparison.OrdinalIgnoreCase) == true)
        {
            double volNA = Helpers.ReadDouble("Enter volume (ml)");
            volume = volNA;
        }

        Food? newFood = FoodFactory.CreateFood(category ?? "", name ?? "", calories, carbs, protein, fats, volume, alcoholPercent);

        if (newFood == null)
        {
            Console.WriteLine("Failed to create food. Check inputs and category.");
            return;
        }

        string foodLine = category.ToLower() switch
        {
            "alcoholic" => $"{name};{calories};{volume};{alcoholPercent};{carbs};{protein};{fats}",
            "nonalcoholic" => $"{name};{calories};{volume};{carbs};{protein};{fats}",
        _   => $"{name};{calories};{carbs};{protein};{fats}"
        };

        availableFoods.Add(newFood);
        FoodFileManager.AppendFoodToFile(foodsFile, category ?? "", foodLine); //replaces null with "" to avoid crash.
        Console.WriteLine("Food added successfully.");
        }
        public static void DisplayFoodsOrganized(List<Food> foods) //Food displaying in menu
        {
            var grouped = foods.GroupBy(f => f.GetType().Name);
            //for every group, get group name
            foreach (var group in grouped)
            {
                Console.WriteLine($"\n-- {group.Key} --"); //isolate food group/type
                foreach (var food in group)
                {
                    food.DisplayInfo();
                }
            }
        }

        public static void AppendFoodToFile(string filepath, string category, string foodLine) //appends/adds to file
        {
            var lines = File.Exists(filepath) ? File.ReadAllLines(filepath).ToList() : new List<string>();
            //check for file again
            int categoryLineIndex = lines.FindIndex(l => l.Trim().Equals($"#{category}", StringComparison.OrdinalIgnoreCase));
            //looks for line in txt file that starts with "#"
            if (categoryLineIndex == -1)
            {
                lines.Add("");
                lines.Add("#" + category);
                lines.Add(foodLine);
            }
            else
            {
                int insertIndex = categoryLineIndex + 1;
                while (insertIndex < lines.Count && !lines[insertIndex].StartsWith("#"))
                {
                    insertIndex++;
                }
                lines.Insert(insertIndex, foodLine);
            }

            File.WriteAllLines(filepath, lines); //overwrites and saves file
        }
    }
}