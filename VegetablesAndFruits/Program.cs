using System.Data.SqlClient;

namespace VegetablesAndFruits
{
    public class Program
    {
        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VegetableFruit;Integrated Security=True;Connect Timeout=30;";

        private static void DisplayChoices()
        {
            Console.WriteLine("\nВиберіть, що бажаєте:");
            Console.WriteLine("1.  Відображення всієї інформації з таблиці овочів і фруктів");
            Console.WriteLine("2.  Відображення усіх назв овочів і фруктів");
            Console.WriteLine("3.  Відображення усіх кольорів");
            Console.WriteLine("4.  Показати максимальну калорійність");
            Console.WriteLine("5.  Показати мінімальну калорійність");
            Console.WriteLine("6.  Показати середню калорійність");
            Console.WriteLine("7.  Показати кількість овочів");
            Console.WriteLine("8.  Показати кількість фруктів");
            Console.WriteLine("9.  Показати кількість овочів і фруктів заданого кольору");
            Console.WriteLine("10. Показати кількість овочів і фруктів кожного кольору");
            Console.WriteLine("11. Показати овочі та фрукти з калорійністю нижче вказаної");
            Console.WriteLine("12. Показати овочі та фрукти з калорійністю вище вказаної");
            Console.WriteLine("13. Показати овочі та фрукти з калорійністю у вказаному діапазоні");
            Console.WriteLine("14. Показати усі овочі та фрукти жовтого або червоного кольору");
            Console.WriteLine("0. Exit");
        }
        private static void FulFillRequest(in SqlConnection connection, in string choice)
        {
            switch (choice)
            {
                case "1":
                    DisplayAllVegetablesAndFruitsInfo(connection);
                    break;

                case "2":
                    DisplayAllVegetablesAndFruitsNames(connection);
                    break;

                case "3":
                    DisplayAllColors(connection);
                    break;

                case "4":
                    DisplayMaxCalories(connection);
                    break;

                case "5":
                    DisplayMinCalories(connection);
                    break;

                case "6":
                    DisplayAvgCalories(connection);
                    break;

                case "7":
                    DisplayVegetablesAmount(connection);
                    break;

                case "8":
                    DisplayFruitsAmount(connection);
                    break;

                case "9":
                    Console.WriteLine("Введіть колір: ");
                    string? color = Console.ReadLine();

                    DisplayAmountOfVegetablesAndFruitsByColor(connection, color);
                    break;

                case "10":
                    DisplayAmountOfVegetablesAndFruitsOfEachColor(connection);
                    break;

                case "11":
                    Console.WriteLine("Введіть калорійність: ");
                    int calories1 = int.Parse(Console.ReadLine());

                    DisplayVegetablesAndFruitsWithCaloriesRequired(connection, calories1, '<');
                    break;

                case "12":
                    Console.WriteLine("Введіть калорійність: ");
                    int calories2 = int.Parse(Console.ReadLine());

                    DisplayVegetablesAndFruitsWithCaloriesRequired(connection, calories2, '>');
                    break;

                case "13":
                    Console.WriteLine("Введіть початок діапазону: ");
                    int start = int.Parse(Console.ReadLine());

                    Console.WriteLine("Введіть кінець діапазону: ");
                    int end = int.Parse(Console.ReadLine());

                    DisplayVegetablesAndFruitsWithCaloriesInRequiredRange(connection, start, end);
                    break;

                case "14":
                    DisplayRedAndYellowVegetablesAndFruits(connection);
                    break;

                case "0":
                    Console.WriteLine("До побачення...");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Неправильний вибір, спробуйте ще");
                    break;
            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Підключення до бази данних...");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Підключення успішне");

                    while (true)
                    {
                        DisplayChoices();

                        string choice = Console.ReadLine();

                        Console.ForegroundColor = ConsoleColor.Cyan;

                        FulFillRequest(connection, choice);

                        Console.ForegroundColor = ConsoleColor.White;

                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }


        private static void DisplayAllVegetablesAndFruitsInfo(in SqlConnection connection)
        {
            List<VegetableFruit> items = new List<VegetableFruit>();

            using (SqlCommand command = new SqlCommand("SELECT * FROM VegetableFruit", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VegetableFruit item = new VegetableFruit
                        {
                            Name = reader["Name"].ToString(),
                            Type = reader["Type"].ToString(),
                            Color = reader["Color"].ToString(),
                            Calories = int.Parse(reader["Calories"].ToString())
                        };

                        items.Add(item);
                    }
                }
            }

            foreach (var item in items)
            {
                Console.WriteLine($"Name: {item.Name}, Type: {item.Type}, Color: {item.Color}, Calories: {item.Calories}");
            }
        }

        private static void DisplayAllVegetablesAndFruitsNames(in SqlConnection connection)
        {
            List<string> names = new List<string>();

            using (SqlCommand command = new SqlCommand("SELECT Name FROM VegetableFruit", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        names.Add(reader["Name"].ToString());
                    }
                }
            }

            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }

        private static void DisplayAllColors(in SqlConnection connection)
        {
            List<string> colors = new List<string>();

            using (SqlCommand command = new SqlCommand("SELECT Color FROM VegetableFruit", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        colors.Add(reader["Color"].ToString());
                    }
                }
            }

            foreach (var item in colors.Distinct())
            {
                Console.WriteLine($"{item}");
            }
        }

        private static void DisplayAvgCalories(in SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT AVG(Calories) AS [AvgCalories] FROM VegetableFruit", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    Console.WriteLine(reader["AvgCalories"]);
                }
            }
        }
        private static void DisplayMinCalories(in SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT MIN(Calories) AS [MinCalories] FROM VegetableFruit", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    Console.WriteLine(reader["MinCalories"]);
                }
            }
        }
        private static void DisplayMaxCalories(in SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT MAX(Calories) AS [MaxCalories] FROM VegetableFruit", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    Console.WriteLine(reader["MaxCalories"]);
                }
            }
        }

        private static void DisplayFruitsAmount(in SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) As [FruitsAmount] FROM VegetableFruit WHERE Type = \'Fruit\'", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    Console.WriteLine(reader["FruitsAmount"]);
                }
            }
        }
        private static void DisplayVegetablesAmount(in SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) As [VegetablesAmount] FROM VegetableFruit WHERE Type = \'Vegetable\'", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    Console.WriteLine(reader["VegetablesAmount"]);
                }
            }
        }

        private static void DisplayAmountOfVegetablesAndFruitsByColor(in SqlConnection connection, in string color)
        {
            using (SqlCommand command = new SqlCommand($"SELECT COUNT(*) AS [Amount] FROM VegetableFruit Where Color = \'{color}\'", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    Console.WriteLine(reader["Amount"]);
                }
            }
        }

        private static void DisplayAmountOfVegetablesAndFruitsOfEachColor(in SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand(
                "SELECT Color, Type, COUNT(*) AS [Amount] FROM VegetableFruit GROUP BY Color, Type ORDER BY Color, Type", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"Type: {reader["Type"]}, Color: {reader["Color"]}, Amount: {reader["Amount"]}");
                    }
                }
            }
        }

        private static void DisplayVegetablesAndFruitsWithCaloriesRequired(in SqlConnection connection, int calories, char sign)
        {
            List<VegetableFruit> items = new List<VegetableFruit>();

            using (SqlCommand command = new SqlCommand($"SELECT * FROM VegetableFruit WHERE Calories {sign} {calories}", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VegetableFruit item = new VegetableFruit
                        {
                            Name = reader["Name"].ToString(),
                            Type = reader["Type"].ToString(),
                            Color = reader["Color"].ToString(),
                            Calories = int.Parse(reader["Calories"].ToString())
                        };

                        items.Add(item);
                    }
                }
            }

            foreach (var item in items)
            {
                Console.WriteLine($"Name: {item.Name}, Type: {item.Type}, Color: {item.Color}, Calories: {item.Calories}");
            }
        }

        private static void DisplayRedAndYellowVegetablesAndFruits(in SqlConnection connection)
        {
            List<VegetableFruit> items = new List<VegetableFruit>();

            using (SqlCommand command = new SqlCommand("SELECT * FROM VegetableFruit WHERE Color = \'Red\' OR Color = \'Yellow\'", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VegetableFruit item = new VegetableFruit
                        {
                            Name = reader["Name"].ToString(),
                            Type = reader["Type"].ToString(),
                            Color = reader["Color"].ToString(),
                            Calories = int.Parse(reader["Calories"].ToString())
                        };

                        items.Add(item);
                    }
                }
            }

            foreach (var item in items)
            {
                Console.WriteLine($"Name: {item.Name}, Type: {item.Type}, Color: {item.Color}, Calories: {item.Calories}");
            }
        }

        private static void DisplayVegetablesAndFruitsWithCaloriesInRequiredRange(SqlConnection connection, int start, int end)
        {
            List<VegetableFruit> items = new List<VegetableFruit>();

            using (SqlCommand command = new SqlCommand($"SELECT * FROM VegetableFruit WHERE Calories > {start} AND Calories < {end}", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VegetableFruit item = new VegetableFruit
                        {
                            Name = reader["Name"].ToString(),
                            Type = reader["Type"].ToString(),
                            Color = reader["Color"].ToString(),
                            Calories = int.Parse(reader["Calories"].ToString())
                        };

                        items.Add(item);
                    }
                }
            }

            foreach (var item in items)
            {
                Console.WriteLine($"Name: {item.Name}, Type: {item.Type}, Color: {item.Color}, Calories: {item.Calories}");
            }
        }

    }

    public class VegetableFruit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public int Calories { get; set; }
    }
}