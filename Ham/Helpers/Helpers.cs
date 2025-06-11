namespace CalorieTracker.Helpers
{
    class Helpers
    {
        public static double ReadDouble(string message)
        {
            double value;
            do
            {
                Console.Write(message);
            } while (!double.TryParse(Console.ReadLine(), out value) || value < 0);
            return value;
        }


        public static int ReadInt(string message)
        {
            int value;
            do
            {
                Console.Write(message);
            } while (!int.TryParse(Console.ReadLine(), out value) || value < 0);
            return value;
        }
        public static string ReadString(string message)
        {
            string? input;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }
        
    }
}

