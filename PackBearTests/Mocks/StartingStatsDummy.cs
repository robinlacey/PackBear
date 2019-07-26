using System.Collections.Generic;
using PackBear.Player.Interface;

namespace PackBearTests.Mocks
{
    public class StartingStatsDummy : IStartingStats
    {
        public int OptionsCount { get; set; }
        public Dictionary<string, float> Weights { get; }
        public Dictionary<string, IStartingStat> Stats { get; set; }
    }
}