using System;

namespace Hangman
{
    public class Program
    {
        static void Main(string[] args)
        {
            BeginAndDisplayInfo();
        }

        private static void BeginAndDisplayInfo()
        {
            Console.WriteLine("Hello Stranger! What is your name?");
            var userName = Console.ReadLine().ToUpper();
            Options currentOptions = new Options(userName);
            Console.Clear();
            Console.WriteLine($"Nice to meet you {currentOptions.UserName}!");
            
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("Press [P] to play. Press [O] for options. Press [E] or [ESC] to exit.");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.P:
                        Console.WriteLine($"OK, {currentOptions.UserName}. Let's play!");
                        Game currentGame = new Game(currentOptions);
                        currentGame.StartGame();
                        break;
                    case ConsoleKey.O:
                        Console.Clear();
                        Console.WriteLine("Options:");
                        Console.WriteLine(
                            "Press [D] to play default mode. Press [C] to play continuous mode. Press [E] or [ESC] to exit.");
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.D:
                                currentOptions.Mode = Mode.Default;
                                break;
                            case ConsoleKey.C:
                                currentOptions.Mode = Mode.Continuous;
                                break;
                            default:
                                Console.Clear();
                                break;
                        }

                        break;
                    case ConsoleKey.E:
                        Console.WriteLine($"See you next time {currentOptions.UserName}!");
                        isRunning = false;
                        break;
                    case ConsoleKey.Escape:
                        Console.WriteLine($"See you next time {currentOptions.UserName}!");
                        isRunning = false;
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }
    }
}