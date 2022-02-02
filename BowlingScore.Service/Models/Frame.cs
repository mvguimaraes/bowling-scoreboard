using System.Collections.Generic;

namespace BowlingScore.Service.Models
{
    public class Frame
    {
        public List<Roll> Rolls { get; set; }
        public int Score { get; set; }
    }
}