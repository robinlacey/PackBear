using System.Collections.Generic;

namespace PackBear.Player.Interface
{
    public interface IPlayerStatsToChange
    {
        Dictionary<string, int> Stats { get; set; }
    }
}