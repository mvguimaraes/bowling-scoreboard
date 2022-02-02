using System;
using BowlingScore.Service;

namespace BowlingScore.Client
{
    internal class Program
    {
        private static readonly ScoreboardService Service = new ScoreboardService();

        private static void Main(string[] args)
        {
            Console.WriteLine("Bowling Scoreboard");

            while (!Service.Completed)
                try
                {
                    Console.Write("Knock down pins: ");
                    var pins = Console.ReadLine();
                    Console.WriteLine("");

                    Service.Play(Convert.ToInt32(pins));
                    PrintScoreboard();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            Console.WriteLine("\nGame over");
        }

        private static void PrintScoreboard()
        {
            var scoreboard = Service.GetScoreboard();

            Console.WriteLine("FRAME\t|\tROLL\t|\tPINS\t|\tTOTAL SCORE");

            foreach (var frame in scoreboard)
            {
                foreach (var roll in frame.Value.Rolls)
                    Console.WriteLine($"{roll.FrameNumber}\t|\t{roll.RollNumber}\t|\t{roll.KnockedDownPins}\t|\t");

                Console.WriteLine($"     \t|\t    \t|\t     \t|\t{frame.Value.Score}");
            }
        }
    }
}