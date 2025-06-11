using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalorieTracker.Foods;
//using CalorieTracker.Factory;



namespace CalorieTracker.Files
{
    public static class FoodFileManager
    {
        public static List<Food> LoadFoodsFromFile(string filepath)
        {
            var foods = new List<Food>();
            if (!File.Exists(filepath))
            {
                Console.WriteLine($"Food file {filepath} not found. Starting empty.");
                return foods;
            }

            string[] lines = File.ReadAllLines(filepath);
            string currentCategory = "";

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (line.StartsWith("#"))
                {
                    currentCategory = line.Substring(1).Trim();
                    continue;
                }

                string[] parts = line.Split(';');
                try
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
                    //        foods.Add(new Meat(parts[0], int.Parse(parts[1]), double.Parse(parts[2]), double.Parse(parts[3]), double.Parse(parts[4])));
                            break;
                        case "dairy":
                  //          foods.Add(new Dairy(parts[0], int.Parse(parts[1]), double.Parse(parts[2]), double.Parse(parts[3]), double.Parse(parts[4])));
                            break;
                        case "otheranimalbased":
                    //        foods.Add(new OtherAnimalProduct(parts[0], int.Parse(parts[1]), double.Parse(parts[2]), double.Parse(parts[3]), double.Parse(parts[4])));
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

        public static void AddNewFoodFlow(string foodsFile, List<Food> availableFoods)
        {
        Console.WriteLine("Enter category (Fruit, Vegetable, Alcoholic, NonAlcoholic, Meat, Dairy, OtherAnimalBased):");
        string? category = Console.ReadLine()?.Trim();

        Console.Write("Enter food name: ");
        string? name = Console.ReadLine();

        Console.Write("Enter calories: ");
        if (!int.TryParse(Console.ReadLine(), out int calories))
        {
            Console.WriteLine("Invalid calories input.");
            return;
        }

        Console.Write("Enter carbs (g): ");
        if (!double.TryParse(Console.ReadLine(), out double carbs))
        {
            Console.WriteLine("Invalid carbs input.");
            return;
        }

        Console.Write("Enter protein (g): ");
        if (!double.TryParse(Console.ReadLine(), out double protein))
        {
            Console.WriteLine("Invalid protein input.");
            return;
        }

        Console.Write("Enter fats (g): ");
        if (!double.TryParse(Console.ReadLine(), out double fats))
        {
            Console.WriteLine("Invalid fats input.");
            return;
        }

        double? volume = null;
        double? alcoholPercent = null;

        if (category?.Equals("alcoholic", StringComparison.OrdinalIgnoreCase) == true)
        {
            Console.Write("Enter volume (ml): ");
            if (!double.TryParse(Console.ReadLine(), out double volA))
            {
                Console.WriteLine("Invalid volume input.");
                return;
            }
            volume = volA;

            Console.Write("Enter alcohol percentage: ");
            if (!double.TryParse(Console.ReadLine(), out double alcPercent))
            {
                Console.WriteLine("Invalid alcohol percentage input.");
                return;
            }
            alcoholPercent = alcPercent;
        }
        else if (category?.Equals("nonalcoholic", StringComparison.OrdinalIgnoreCase) == true)
        {
            Console.Write("Enter volume (ml): ");
            if (!double.TryParse(Console.ReadLine(), out double volNA))
            {
                Console.WriteLine("Invalid volume input.");
                return;
            }
            volume = volNA;
        }

        //Food? newFood = FoodFactory.CreateFood(category ?? "", name ?? "", calories, carbs, protein, fats, volume, alcoholPercent);

        //if (newFood == null)
        //{
         //   Console.WriteLine("Failed to create food. Check inputs and category.");
         //   return;
       // }

        // Prepare the line for file saving based on category
        string foodLine = category.ToLower() switch
        {
            "alcoholic" => $"{name};{calories};{volume};{alcoholPercent};{carbs};{protein};{fats}",
            "nonalcoholic" => $"{name};{calories};{volume};{carbs};{protein};{fats}",
        _   => $"{name};{calories};{carbs};{protein};{fats}"
        };

      //  availableFoods.Add(newFood);
        FoodFileManager.AppendFoodToFile(foodsFile, category ?? "", foodLine);
        Console.WriteLine("Food added successfully.");
        }


        public static void DisplayFoodsOrganized(List<Food> foods)
        {
            var grouped = foods.GroupBy(f => f.GetType().Name);

            foreach (var group in grouped)
            {
                Console.WriteLine($"\n-- {group.Key} --");
                foreach (var food in group)
                {
                    food.DisplayInfo();
                }
            }
        }

        public static void AppendFoodToFile(string filepath, string category, string foodLine)
        {
            var lines = File.Exists(filepath) ? File.ReadAllLines(filepath).ToList() : new List<string>();

            int categoryLineIndex = lines.FindIndex(l => l.Trim().Equals($"#{category}", StringComparison.OrdinalIgnoreCase));

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

            File.WriteAllLines(filepath, lines);
        }
    }
}
