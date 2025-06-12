using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using CalorieTracker.Foods;
using CalorieTracker.Iface;

namespace CalorieTracker.Users
{
    public class User : IUser
    {
        public string Username { get; set; } = string.Empty;
        public List<Food> EatenFoods { get; set; } = new List<Food>();

        public void AddFood(Food food)
        {
            EatenFoods.Add(food);
        }

        public int TotalCaloriesToday()
        {
            return EatenFoods.Sum(f => f.Calories);
        }

        public void SaveToFile()
        {
            string filename = Path.Combine(AppContext.BaseDirectory, Username + ".json");
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText(filename, json);
            Console.WriteLine($"User data saved to: {filename}");
        }

        public static User LoadFromFile(string username)
        {
            string filename = Path.Combine(AppContext.BaseDirectory, username + ".json");
            Console.WriteLine($"Loading user data from: {filename}");

            if (File.Exists(filename))
            {
                try
                {
                    string json = File.ReadAllText(filename);
                    var options = new JsonSerializerOptions();
                    var user = JsonSerializer.Deserialize<User>(json, options);

                    if (user == null)
                    {
                        Console.WriteLine("Deserialization returned null, creating new user.");
                        return new User { Username = username };
                    }

                    Console.WriteLine($"Loaded user: {user.Username}, Foods count: {user.EatenFoods.Count}");
                    return user;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to load user data: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("User file not found, creating new user.");
            }

            return new User { Username = username };
        }

        public void DisplayStats()
        {
            Console.WriteLine($"User: {Username}");
            Console.WriteLine($"Total calories consumed today: {TotalCaloriesToday()} kcal");
        }
    }
}