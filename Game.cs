using System;

namespace Hangman
{
    class Game
    {
        public Status Status { get; set; }
        private Options Options { get; set; }
        public int Rounds { get; set; }
        public int Score { get; private set; }
        public int TotalTime { get; private set; }

        public Game(Options options)
        {
            Options = options;
            Status = Status.Ongoing;
            Score = 0;
            Rounds = 0;
        }

        internal void StartGame()
        {
            DateTime timeStamp = DateTime.Now;
            while (Status == Status.Ongoing)
            {
                Round currentRound = new Round(Options.Solutions[Options.RandomSolutionIndex()]);
                currentRound.StartRound();
                if (Options.Mode == Mode.Default || currentRound.Status == Status.Lost)
                {
                    Status = currentRound.Status;
                }
                else
                {
                    Score++;
                }

                Rounds++;
            }

            TotalTime = (DateTime.Now - timeStamp).Seconds;
            Console.WriteLine($"The game took you {TotalTime} seconds");
        }
    }
}