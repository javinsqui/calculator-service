using CalculatorService.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace CalculatorService.Client
{
    class Program
    {
        public int idTrack = 0;

        static void Main(string[] args)
        {
            bool showMenu = true;
            
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            string result = "";
            bool ret = true;

            Console.Clear();
            Console.WriteLine("\n");
            Console.WriteLine("------------------------\n");
            Console.WriteLine("       Calculator\n");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("   1) Add");
            Console.WriteLine("   2) Substract");
            Console.WriteLine("   3) Multiply");
            Console.WriteLine("   4) Divide");
            Console.WriteLine("   5) Square root");
            Console.WriteLine("   6) Query");
            Console.WriteLine("   0) Exit");
            Console.WriteLine("------------------------\n");
            Console.Write("Select an option (0 to quit): ");

            ConsoleKeyInfo cki = Console.ReadKey();
            var opt = cki.KeyChar.ToString();
            int i = 0;
            if (!int.TryParse(opt, out i) || (i < 0 || i > 6) )
                Console.Write($"{opt} is not a valid option.");
            else
            {
                string id = "";
                // Only when calc
                if (i > 0 && i < 6)
                {
                    Console.Write("\nDo you want tracking it? [N/y]: ");
                    cki = Console.ReadKey();
                    var yn = cki.KeyChar.ToString();
                    if (yn == "y" || yn == "Y")
                    {
                        Console.Write("\nEnter Id : ");
                        id = Console.ReadLine();
                    }
                }

                switch (opt)
                {
                    case "0":
                        ret = false;
                        break;
                    case "1":
                        result = Calc.Add(id);
                        break;
                    case "2":
                        result = Calc.Sub(id);
                        break;
                    case "3":
                        result = Calc.Mult(id);
                        break;
                    case "4":
                        result = Calc.Div(id);
                        break;
                    case "5":
                        result = Calc.Sqrt(id);
                        break;
                    case "6":
                        result = Calc.Query();
                        break;
                    default:
                        Console.Write($"\n{opt} is not a valid option.");
                        break;
                }

                if (i > 0 && i < 6)
                    Console.WriteLine($"result:: {result}");
                else if (i == 6)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    List<Operations> operations = JsonSerializer.Deserialize<List<Operations>>(result, options);
                    if (operations != null)
                    {
                        Console.WriteLine("\nOperations: ");
                        foreach (Operations op in operations)
                        {
                            Console.WriteLine($"\tOperation: {op.Operation}, Calculation: {op.Calculation}, Date: {op.Date}");
                        }
                    }
                }
            }

            Console.Write("\nPress any key to clear results ...");
            Console.ReadKey();

            return ret;
        }


    }
}
