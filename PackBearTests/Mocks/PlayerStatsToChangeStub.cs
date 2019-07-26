using System.Collections.Generic;
using PackBear.Player.Interface;

namespace PackBearTests.Mocks
{
    public class PlayerStatsToChangeStub : IPlayerStatsToChange
    {
        public PlayerStatsToChangeStub(Dictionary<string, int> stats)
        {
            Stats = stats;
        }

        public Dictionary<string, int> Stats { get; set; }
    }
}