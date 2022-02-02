using System.Collections.Generic;
using BowlingScore.Service.Models;

namespace BowlingScore.Service.Handlers
{
    public interface IFrameHandler
    {
        void AddRoll(List<Roll> rolls, int frameNumber, int rollNumber, int knockedDownPins);
    }
}