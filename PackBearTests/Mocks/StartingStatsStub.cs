using System.Collections.Generic;
using PackBear.Player.Interface;

namespace PackBearTests.Mocks
{
    public class StartingStatsStub : IStartingStats
    {
        public StartingStatsStub(Dictionary<string, IStartingStat> stats, Dictionary<string, float> weights,
            int optionsCount)
        {
            Stats = stats;
            Weights = weights;
            OptionsCount = optionsCount;
        }

        public int OptionsCount { get; set; }
        public Dictionary<string, float> Weights { get; }
        public Dictionary<string, IStartingStat> Stats { get; set; }
    }
}