using System;
using System.Collections.Generic;
using System.Linq;
using BowlingScore.Service.Handlers;
using BowlingScore.Service.Models;

namespace BowlingScore.Service
{
    public class ScoreboardService
    {
        private readonly IFrameHandler _regularFrameService;
        private readonly IFrameHandler _tenthFrameService;

        public ScoreboardService()
        {
            Rolls = new List<Roll>();

            _regularFrameService = new RegularFrameHandler();
            _tenthFrameService = new TenthFrameHandler();
        }

        public List<Roll> Rolls { get; internal set; }
        public bool Completed => Rolls.Count == 21;

        public void Play(int knockedDownPins)
        {
            if (Completed)
                throw new InvalidOperationException("Game over");

            int frameNumber = 0, rollNumber = 0;

            var previousRoll = Rolls.LastOrDefault();

            if (previousRoll == null)
            {
                frameNumber = 1;
                rollNumber = 1;
            }
            else
            {
                switch (previousRoll.RollNumber)
                {
                    case 1:
                        frameNumber = previousRoll.FrameNumber;
                        rollNumber = 2;
                        break;
                    case 2 when previousRoll.FrameNumber < 10:
                        frameNumber = previousRoll.FrameNumber + 1;
                        rollNumber = 1;
                        break;
                    case 2 when previousRoll.FrameNumber == 10:
                        frameNumber = previousRoll.FrameNumber;
                        rollNumber = 3;
                        break;
                }
            }

            AddRoll(frameNumber, rollNumber, knockedDownPins);
        }

        private void AddRoll(int frame, int roll, int knockedDownPins)
        {
            if (knockedDownPins < 0 || knockedDownPins > 10)
                throw new InvalidOperationException("Value must be between 0 and 10");

            if (frame < 10)
                _regularFrameService.AddRoll(Rolls, frame, roll, knockedDownPins);
            else
                _tenthFrameService.AddRoll(Rolls, frame, roll, knockedDownPins);
        }

        public Dictionary<int, Frame> GetScoreboard()
        {
            var scoreboard = Rolls
                .GroupBy(x => x.FrameNumber)
                .ToDictionary(g => g.Key, g => new Frame
                {
                    Rolls = g.ToList(),
                    Score = 0
                });

            CalculateScore(scoreboard);

            return scoreboard;
        }

        private static void CalculateScore(Dictionary<int, Frame> scoreboard)
        {
            foreach (var frame in scoreboard.Keys.ToList())
            {
                var previousFrame = scoreboard.ContainsKey(frame - 1) ? scoreboard[frame - 1] : null;

                var nextFrame = scoreboard.ContainsKey(frame + 1) ? scoreboard[frame + 1] : null;

                // ordinary
                var currentScore = scoreboard[frame].Rolls.Sum(x => x.KnockedDownPins ?? 0);

                // spare
                if (currentScore == 10 &&
                    scoreboard[frame].Rolls.All(i => i.KnockedDownPins.HasValue && i.KnockedDownPins > 0))
                    if (nextFrame != null)
                        currentScore += nextFrame.Rolls.Find(i => i.RollNumber == 1).KnockedDownPins ?? 0;

                // strike
                if (currentScore == 10 && scoreboard[frame].Rolls[0].KnockedDownPins == 10)
                    if (nextFrame != null)
                    {
                        if (nextFrame.Rolls[0].KnockedDownPins < 10)
                        {
                            currentScore += nextFrame.Rolls[0].KnockedDownPins ?? 0;
                            currentScore += nextFrame.Rolls[1].KnockedDownPins ?? 0;
                        }
                        else
                        {
                            currentScore += nextFrame.Rolls[0].KnockedDownPins ?? 0;

                            if (scoreboard.ContainsKey(frame + 2))
                                currentScore += scoreboard[frame + 2].Rolls[0].KnockedDownPins ?? 0;
                            else
                            {
                                var next = nextFrame.Rolls.ElementAtOrDefault(1);
                                currentScore += next?.KnockedDownPins ?? 0;
                            }
                        }
                    }

                scoreboard[frame].Score = currentScore + (previousFrame?.Score ?? 0);
            }
        }
    }
}