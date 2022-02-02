using System;
using System.Collections.Generic;
using System.Linq;
using BowlingScore.Service.Models;

namespace BowlingScore.Service.Handlers
{
    public class TenthFrameHandler : IFrameHandler
    {
        public void AddRoll(List<Roll> rolls, int frame, int roll, int knockedDownPins)
        {
            var availablePins = 10;

            var frameRolls = rolls.Where(i => i.FrameNumber == 10).ToList();

            switch (roll)
            {
                case 1:
                case 2 when frameRolls[0].KnockedDownPins == 10:
                    availablePins = 10;
                    break;
                case 2:
                    availablePins = 10 - (frameRolls[0].KnockedDownPins ?? 0);
                    break;
                case 3:
                {
                    var previousPinCount = frameRolls.Sum(i => i.KnockedDownPins ?? 0);

                    if (previousPinCount == 10 || previousPinCount == 20)
                        availablePins = 10;
                    else
                        availablePins = 10 - (frameRolls[1].KnockedDownPins ?? 0);
                    break;
                }
            }

            if (knockedDownPins > availablePins)
                throw new InvalidOperationException($"Invalid value. You can knock down up to {availablePins} pin(s)");

            rolls.Add(new Roll(frame, roll, knockedDownPins));

            if (roll != 2) return;
            {
                var frameScore = rolls.Where(i => i.FrameNumber == 10).Sum(i => i.KnockedDownPins ?? 0);

                if (frameScore == 10 || frameScore == 20) return;

                rolls.Add(new Roll(frame, 3, null));
            }
        }
    }
}