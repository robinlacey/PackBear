using System.Collections.Generic;

namespace PackBear.Player.Interface
{
    public interface IStartingStats
    {
        int OptionsCount { get; set; }
        Dictionary<string, float> Weights { get; }
        Dictionary<string, IStartingStat> Stats { get; set; }
    }
}