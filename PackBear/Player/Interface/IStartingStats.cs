using System.Collections.Generic;
using DealerBear.Player;

namespace PackBear.Player.Interface
{
    public interface IStartingStats
    {
        Dictionary<string, IStat> Stats { get; set; }
    }
}