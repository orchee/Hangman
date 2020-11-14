using System;
using System.Collections.Generic;

namespace Hangman
{
    class Round
    {
        private const int DefaultLifeCount = 5;

        public Round(Solution solution, int lives = DefaultLifeCount)
        {
            Solution = solution;
            Status = Status.Ongoing;
            _lives = lives;
        }

        public Status Status { get; private set; }
        private Solution Solution { get; set; }

        private int _lives;

        private List<char> _lettersGuesses = new List<char>();

        private List<char> _lettersGuessed = new List<char>();

        private List<string> _wordGuesses = new List<string>();

        private string GetPuzzle()
        {
            var puzzle = new List<char>();
            foreach (var solutionLetter in Solution.Word.ToCharArray())
            {
                if (_lettersGuessed.Contains(solutionLetter))
                {
                    puzzle.Add(solutionLetter);
                }
                else if (solutionLetter.ToString() == " ")
                {
                    puzzle.Add(' ');
                }
                else
                {
                    puzzle.Add('_');
                }
            }

            return string.Join(" ", puzzle.ToArray());
        }

        internal void StartRound()
        {
            while (Status == Status.Ongoing)
            {
                Console.Clear();
                Console.WriteLine($"Lives left: {_lives}");
                Console.WriteLine(GetPuzzle());
                Console.WriteLine($"Letters you've tried: {SeparatedChars(_lettersGuesses)}");
                Console.WriteLine($"Words you've tried: {SeparatedWords(_wordGuesses)}");
                if (_lives == 1)
                {
                    Console.WriteLine($"Hint: {Solution.Hint}");
                }

                Console.WriteLine(
                    "To choose a letter press [L], to try to guess a word [W]. Press [E] or [ESC] to exit.");
                string guess;
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.L:
                        Console.WriteLine("Letter:");
                        guess = Console.ReadKey().KeyChar.ToString().ToUpper();
                        _lettersGuesses.Add(guess[0]);
                        if (IsGuessCorrect(guess))
                        {
                            _lettersGuessed.Add(guess[0]);
                            if (IsGuessCorrect(GetPuzzle().Replace(" ", "")))
                            {
                                Status = Status.Won;
                                HandleCorrectAnswer();
                            }
                        }
                        else
                        {
                            _lives--;
                        }

                        break;
                    case ConsoleKey.W:
                        Console.WriteLine("Word:");
                        var word = Console.ReadLine();
                        if (string.IsNullOrEmpty(word)) continue;
                        guess = word.ToUpper();
                        if (IsGuessCorrect(guess))
                        {
                            Status = Status.Won;
                            HandleCorrectAnswer();
                        }
                        else
                        {
                            _wordGuesses.Add(guess);
                            _lives -= 2;
                        }

                        break;
                    case ConsoleKey.E:
                        Status = Status.Lost;
                        break;
                    case ConsoleKey.Escape:
                        Status = Status.Lost;
                        break;
                    default:
                        Console.Clear();
                        break;
                }

                Console.Clear();
                if (_lives < 1)
                {
                    Status = Status.Lost;
                }
            }
        }

        private void HandleCorrectAnswer()
        {
            Console.WriteLine("Congratulations, correct answer!");
            Console.ReadKey();
        }

        private bool IsGuessCorrect(string guess)
        {
            return (guess.Length > 1 && Solution.Word == guess) ||
                   (guess.Length == 1 && Solution.Word.Contains(guess));
        }

        // helpers
        private static string SeparatedChars(List<char> chars)
        {
            return string.Join(",", chars);
        }

        private static string SeparatedWords(List<string> strings)
        {
            return string.Join(",", strings);
        }
    }
}