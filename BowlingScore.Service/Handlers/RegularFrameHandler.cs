using System;
using System.Collections.Generic;
using System.Linq;
using BowlingScore.Service.Models;

namespace BowlingScore.Service.Handlers
{
    public class RegularFrameHandler : IFrameHandler
    {
        public void AddRoll(List<Roll> rolls, int frameNumber, int rollNumber, int knockedDownPins)
        {
            var pinsAlreadyKnockedDown = rolls.Where(i => i.FrameNumber == frameNumber).Sum(i => i.KnockedDownPins);

            var proposedPinCount = pinsAlreadyKnockedDown + knockedDownPins;

            if (proposedPinCount > 10)
                throw new InvalidOperationException(
                    $"Invalid value. You can knock down up to {10 - pinsAlreadyKnockedDown} pin(s)");

            rolls.Add(new Roll(frameNumber, rollNumber, knockedDownPins));

            //if strike, add an empty(KnockedDownPins == null) roll
            if (rollNumber == 1 && knockedDownPins == 10) rolls.Add(new Roll(frameNumber, 2, null));
        }
    }
}