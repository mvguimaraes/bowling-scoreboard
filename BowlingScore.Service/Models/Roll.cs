namespace BowlingScore.Service.Models
{
    public class Roll
    {
        public Roll(int frameNumber, int rollNumber, int? knockedDownPins)
        {
            FrameNumber = frameNumber;
            RollNumber = rollNumber;
            KnockedDownPins = knockedDownPins;
        }

        public int FrameNumber { get; set; }
        public int RollNumber { get; set; }
        public int? KnockedDownPins { get; set; }
    }
}