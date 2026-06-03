using System;
using System.Diagnostics;
using System.IO;


namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("/------------------------------\\");
            Console.WriteLine("| WELLCOME TO A GAME OF HANGMAN|");
            Console.WriteLine("\\------------------------------/");
            while (true)
            {
                int responce = PrintOptions();

                switch (responce)
                {
                    case 1:
                        //New game
                        break;
                    case 2:
                        //Game history
                        break;
                    case 3:
                        //Rules
                        break;
                    case 4:
                        //Exit
                        Console.WriteLine("Have a great day!!!");
                        return;
                    case 745748379:
                        //easteregg
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;

                }
                Console.Clear();
            }


        }
        static int PrintOptions()
        {
            while (true)
            {
                Console.WriteLine("1.New game");
                Console.WriteLine("2.Game history");
                Console.WriteLine("3.Ruls");
                Console.WriteLine("4.Exit");

                string input = Console.ReadLine();
                int responce = 0;
                if (int.TryParse(input, out responce))
                {
                    return responce;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
        static void DrawHangman(int mistakes)
        {
            Console.WriteLine(" +---+");
            Console.WriteLine(" |   |");

            if (mistakes >= 1)
                Console.WriteLine(" O   |");
            else
                Console.WriteLine("     |");

            if (mistakes == 2)
                Console.WriteLine(" |   |");
            else if (mistakes == 3)
                Console.WriteLine("/|   |");
            else if (mistakes >= 4)
                Console.WriteLine("/|\\  |");
            else
                Console.WriteLine("     |");

            if (mistakes == 5)
                Console.WriteLine("/    |");
            else if (mistakes >= 6)
                Console.WriteLine("/ \\  |");
            else
                Console.WriteLine("     |");

            Console.WriteLine("     |");
            Console.WriteLine("=========");
        }
        static void PrintRules()
        {
            if (File.Exists("gameruls.txt"))
            {
                Console.WriteLine(File.ReadAllText("gameruls.txt"));
            }
            else
            {
                Console.WriteLine("gameruls.txt not found!");
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }



    }
}